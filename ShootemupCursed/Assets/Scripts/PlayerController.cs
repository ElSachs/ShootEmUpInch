using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
    Queue<PoolManager.Generate> bonusQueue = new Queue<PoolManager.Generate>();
    [SerializeField] private float bonusTime = 5;
    public GameObject redCube;
    public GameObject blueCube;
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
        Move();
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
                
                //Vector3 pos = new Vector3(transform.position.x, transform.position.y + 12f, transform.position.z);
                Rigidbody2D shotBullet = PoolManager.Instance.spawnFromPool(PoolManager.Generate.normalBullet, Spawner[i]);
                shotBullet.AddForce(Vector2.up * bulletSpeed);

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
                gameObject.layer = 12;
                
            }
            else
            {
                Isblue = true;
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

        if (bonusQueue.Count != 0)
        {
            if (bonusTime > 0)
            {
                bonusTime -= Time.deltaTime*2f;
            }
            else
            {
                bonusTime = 10;
                PoolManager.Generate bonus = bonusQueue.Dequeue();
                switch (bonus)
                {
                    case PoolManager.Generate.shootBullet :
                        shootBullet--;
                        break;
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
            move = Vector3.Lerp(move, Vector3.zero, decelerationSpeed * Time.deltaTime);
        }


        self.velocity = move * maxSpeed * Time.deltaTime;
    }

    public void AddBonus(PoolManager.Generate bonus)
    {
        bonusQueue.Enqueue(bonus);
        switch (bonus)
        {
            case PoolManager.Generate.shootBullet :
                shootBullet++;
                break;
        }
    }
}
