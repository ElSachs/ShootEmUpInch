using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public BulletScriptable stat;
    [NonSerialized]public Rigidbody2D rb;
    private Vector2 triangleToPlayer;
    private float notMoving;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void OnDisable()
    {
        rb.velocity = Vector2.zero;
    }

    private void Update()
    {
        if (rb.velocity == Vector2.zero)
        {
            notMoving++;
        }

        if (notMoving >= 500f)
        {
            gameObject.SetActive(false);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Player" :
                other.GetComponent<PlayerController>().life--;
                gameObject.SetActive(false);
                GameManager.Instance.UpdateLife();
                break;
            
            case "Triangle" :
                other.GetComponent<TriangleBehaviour>().life--;
                gameObject.SetActive(false);
                break;
            
            case "Cube" :
                if (other.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
                {
                    other.GetComponent<CubeBehaviour>().life--;
                }

                gameObject.SetActive(false);
                break;
            
            case "Triple" :
                other.GetComponent<TripleBallBehaviour>().life--;
                gameObject.SetActive(false);
                break;
            
            case "BulletDestroyer" :
                gameObject.SetActive(false);
                break;
            
            case "Penta" :
                other.GetComponent<PentaBehaviour>().life--;
                gameObject.SetActive(false);
                break;
            
            default :
                Debug.Log("j'ai touché");
                other.GetComponent<EnemyBehaviour>().life--;
                gameObject.SetActive(false);
                break;
        }
        /*if (other.tag == "Player")
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
        else if (other.tag == "BulletDestroyer")
        {
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("j'ai touché");
            other.GetComponent<EnemyBehaviour>().life--;
            gameObject.SetActive(false);
        }*/
        
        
    }
}
