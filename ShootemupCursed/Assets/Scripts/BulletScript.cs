using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public BulletScriptable stat;
    [NonSerialized]public Rigidbody2D rb;
    private bool isStart = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.down * stat.bulletSpeed);
        isStart = false;
    }

    private void OnEnable()
    {
        if (!isStart)
        { 
            rb.AddForce(Vector2.down * stat.bulletSpeed);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
         other.GetComponent<PlayerController>().life--; 
         gameObject.SetActive(false);
        }
        else if(other.tag == "Triangle")
        {
            other.GetComponent<TriangleBehaviour>().life--;
            gameObject.SetActive(false);
            
        }
        else if(other.tag == "Cube")
        {
            other.GetComponent<CubeBehaviour>().life--;
           gameObject.SetActive(false);
            
        }
        else if(other.tag == "Triple")
        {
            other.GetComponent<TripleBallBehaviour>().life--;
           gameObject.SetActive(false);
        }
        else
        {
            other.GetComponent<EnemyBehaviour>().life--;
           gameObject.SetActive(false);
        }
        
    }
}
