using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [NonSerialized] public bool canMove = true;
    [NonSerialized] public bool isTransiting;
    private int transitTimer = 0;
    private float vroom = 0.1f;
    
    private Vector3 input;
    private Vector3 move;
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


    public AudioSource shoot;
    public AudioSource swap;
    
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

        if (isTransiting)
        {
            if (transform.position.x > 0.5)
            {
                transform.Translate(-0.5f, 0f, 0f);
            }
            else if (transform.position.x < -0.5)
            {
                transform.Translate(-0.5f, 0f, 0f);
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
                        vroom = vroom * 1.5f;
                    }
                    else
                    {
                        vroom = 0.1f;
                        transitTimer = 100;
                        isTransiting = false;
                        canMove = true;
                    }
                }
            }
        }
        
        if (life <= 0)
        {
            GameObject.Find("Main Camera").GetComponent<SoundController>().PlayDeathSound();
            gameObject.SetActive(false);
            gameOverCanvas.SetActive(true);
        }

        if (invincibilityFrame)
        {
            Instantiate(invincibilityCircle, transform.position, Quaternion.identity);
            invincibilityFrame = false;
        }

        if (Input.GetKey(KeyCode.Space) && coolDown <= 0f)
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

        if (Input.GetKeyDown(KeyCode.LeftShift))
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            shoot.Play();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            shoot.Stop();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            decelerationSpeed = 100;
            maxSpeed -= 5f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            decelerationSpeed = 5;
            maxSpeed += 5;

        }
    }
    

    private void FixedUpdate()
    {
        self.velocity = move * maxSpeed;
        
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
        switch (bonus)
        {
            case PoolManager.Generate.shootBullet :
                shootQueue.Enqueue(bonus);
                shootBullet++;
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
}
