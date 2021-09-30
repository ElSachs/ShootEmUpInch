using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [NonSerialized] public bool canMove = true;
    public bool spawnShip;
    [NonSerialized] public bool isTransiting;
    private int transitTimer = 0;
    public float vroom = 0.1f;
    
    private Vector3 input;
    public Vector3 move;
    [SerializeField] float decelerationSpeed;
    [SerializeField] private float maxSpeed;
    private Rigidbody2D self;
    public int life = 3;
    public float attackSpeed = 5;
    public int shootBullet = 1;
    [SerializeField] float SwitchSpeed;
    public float coolDown = 0;
    public Transform[] Spawner;
    public bool Isblue = false;
    bool isDead;
    
    Queue<PoolManager.Generate> shootQueue = new Queue<PoolManager.Generate>();
    Queue<PoolManager.Generate> speedQueue = new Queue<PoolManager.Generate>();
    Queue<PoolManager.Generate> shieldQueue = new Queue<PoolManager.Generate>();

    [SerializeField] private int shootQueues;
    [SerializeField] private int speedQueues;
    [SerializeField] private int shieldQueues;

    [SerializeField] private float shootTime = 5;
    [SerializeField] private float speedTime = 5;
    [SerializeField] private float shieldTime = 5;
    public GameObject gameOverCanvas;
    public ParticleSystem deathParticle;
    public GameObject playerModel;

    public AudioSource shoot;
    public AudioSource swap;
    public AudioSource bonusSound;
    
    [SerializeField] private float bulletSpeed;
    public bool invincibilityFrame = false;
    [SerializeField] private GameObject invincibilityCircle; 
    private void Start()
    {
        self = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        shootQueues = shootQueue.Count;
        speedQueues = speedQueue.Count;
        shieldQueues = shieldQueue.Count;
        if (canMove)
        {
            Move();
        }

        
        
        if (life <= 0 && !isDead)
        {
            isDead = true;
            gameOverCanvas.SetActive(true);
            StartCoroutine(Death());
        }

        if (invincibilityFrame)
        {
            Instantiate(invincibilityCircle, transform.position, Quaternion.identity);
            invincibilityFrame = false;
        }

        if (Input.GetKey(KeyCode.Space) && coolDown <= 0f && GameManager.Instance.shootEnable)
        {
            for (int i = 0; i < shootBullet; i++)
            {
                if (Spawner.Length > i)
                {
                    
                //Vector3 pos = new Vector3(transform.position.x, transform.position.y + 12f, transform.position.z);
                Rigidbody2D shotBullet = PoolManager.Instance.spawnFromPool(PoolManager.Generate.normalBullet, Spawner[i]);
                shotBullet.AddForce(Vector2.up * bulletSpeed);
                }
                else
                {
                    Debug.Log("MaxShoot");
                }

            }
            coolDown = 2;
        }

        if (coolDown>0)
        {
            coolDown -= Time.deltaTime * attackSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Isblue)
            {
                Isblue = false;
                GameManager.Instance.UpdateBorder(false);
                gameObject.layer = 12;
                
            }
            else
            {
                Isblue = true;
                GameManager.Instance.UpdateBorder(true);
                gameObject.layer = 3;
                
            }
            swap.Play();
        }
        if (Isblue && transform.rotation.eulerAngles.y < 175)
        {
            transform.Rotate(0f, 200f*Time.deltaTime*SwitchSpeed, 0f);
        }
        else if (!Isblue && transform.rotation.eulerAngles.y > 5)
        {
            transform.Rotate(0f, -200f*Time.deltaTime*SwitchSpeed, 0f);
        }

        if (shootQueue.Count != 0)
        {
            if (shootTime > 0)
            {
                shootTime -= Time.deltaTime*2f;
            }
            else
            {
                shootTime = 10;
                PoolManager.Generate bonus = shootQueue.Dequeue();
                shootBullet--;
            }
        }
        if (speedQueue.Count != 0)
        {
            if (speedTime > 0)
            {
                speedTime -= Time.deltaTime*2f;
            }
            else
            {
                speedTime = 10;
                PoolManager.Generate bonus = speedQueue.Dequeue();
                attackSpeed-=3;
            }
        }
        if (shieldQueue.Count != 0)
        {
            if (shieldTime > 0)
            {
                shieldTime -= Time.deltaTime*2f;
            }
            else
            {
                shieldTime = 10;
                PoolManager.Generate bonus = shieldQueue.Dequeue();
                if (shieldQueue.Count == 0)
                {
                    gameObject.GetComponent<BoxCollider2D>().enabled = true;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && GameManager.Instance.shootEnable && !shoot.isPlaying)
        {
            shoot.Play();
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) || !GameManager.Instance.shootEnable)
        {
            shoot.Stop();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            decelerationSpeed = 100f;
            maxSpeed -= 10f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            decelerationSpeed = 5;
            maxSpeed += 10;

        }
    }
    

    private void FixedUpdate()
    {
        self.velocity = move * maxSpeed;
        
        if (isTransiting)
        {
            if (transform.position.x > 0.1)
            {
                transform.Translate(-0.01f, 0f, 0f);
            }
            else if (transform.position.x < -0.1)
            {
                transform.Translate(+0.01f, 0f, 0f);
            }
            else
            {
                if (transitTimer >= 0)
                {
                    transitTimer--;
                }
                else
                {
                    if (transform.position.y < 7)
                    {
                        transform.Translate(0f, vroom, 0f);
                    }
                    else
                    {
                        vroom = 0.1f;
                        transitTimer = 100;
                        isTransiting = false;
                        canMove = true;
                        GameManager.Instance.spawnShip = true;
                    }
                }
            }
        }

        if (spawnShip)
        {
            if (transform.position.y < -4 )
            {
                Debug.Log("avance");
                transform.Translate(0f, vroom, 0f);
                Debug.Log("avancé");
            }
            else
            {
                spawnShip = false;
                GetComponent<BoxCollider2D>().enabled = true;
                WaveManager.Instance.level++;
                WaveManager.Instance.waveType = 0;
            }
        }
    }

    void Move()
    {
        input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);

        if (input != Vector3.zero)
        {
            move = input;
        }
        else
        {
            move = Vector3.Lerp(move, Vector3.zero, decelerationSpeed);
        }


    }
    
    

    public void AddBonus(PoolManager.Generate bonus)
    {
        bonusSound.Play();

        switch (bonus)
        {
            case PoolManager.Generate.shootBullet :
                shootQueue.Enqueue(bonus);
                if(shootBullet < 5) shootBullet++;
                break;
            
            case  PoolManager.Generate.SpeedBullet :
                speedQueue.Enqueue(bonus);
                attackSpeed += 3;
                break;
            
            case  PoolManager.Generate.shield :
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                shieldQueue.Enqueue(bonus);
                break;
        }
    }

    IEnumerator Death()
    {
        GameObject.Find("Main Camera").GetComponent<SoundController>().PlayDeathSound();
        deathParticle.Play();
        playerModel.SetActive(false);
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
