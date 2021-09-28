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
    public float coolDown = 0;
    public Transform Spawner;
    public bool Isblue = true;
    
    public GameObject redCube;
    public GameObject blueCube;

    [SerializeField] private float bulletSpeed;
    private void Start()
    {
        self = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        if (life <= 0)
        {
            Debug.Log("Game Over");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        { 
            //Vector3 pos = new Vector3(transform.position.x, transform.position.y + 12f, transform.position.z);
            Rigidbody2D shotBullet = PoolManager.Instance.spawnFromPool(PoolManager.Generate.normalBullet, Spawner);
            shotBullet.AddForce(Vector2.up * bulletSpeed);
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
                redCube.SetActive(true);
                blueCube.SetActive(false);
            }
            else
            {
                Isblue = true;
                gameObject.layer = 3;
                blueCube.SetActive(true);
                redCube.SetActive(false);
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
            move = Vector3.Lerp(move, Vector3.zero, decelerationSpeed * Time.deltaTime);
        }


        self.velocity = move * maxSpeed * Time.deltaTime;
    }
}
