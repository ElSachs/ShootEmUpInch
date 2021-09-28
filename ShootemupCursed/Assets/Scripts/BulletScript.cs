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
<<<<<<< Updated upstream
        Debug.Log("enter Collider");
         
       if (other.tag == "Player")
=======
        Debug.Log(other.name);
        if (other.tag == "Player")
>>>>>>> Stashed changes
        {
         other.GetComponent<PlayerController>().life--; 
         gameObject.SetActive(false);
         Debug.Log("touch player");
        }
        else if (other.tag == "Untagged")
        {
           gameObject.SetActive(false);
            
        }
        else
        {
            Debug.Log("touch Enemy");
            other.GetComponent<EnemyBehaviour>().life--;
            gameObject.SetActive(false);
            
        }
        
        
    }
}
