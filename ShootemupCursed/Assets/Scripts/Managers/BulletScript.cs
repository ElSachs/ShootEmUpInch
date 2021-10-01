using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public BulletScriptable stat;
    [NonSerialized] public Rigidbody2D rb;
    private Vector2 triangleToPlayer;
    private float notMoving;
    private Transform waveManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        waveManager = GameObject.Find("WaveManager").transform;
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
        if (GameManager.Instance.shootEnable)
        {

            
            //Debug.Log(gameObject.name + " touch " + other.name);
            switch (other.tag)
            {
                case "Player":

                    other.GetComponent<PlayerController>().LooseLife();

                    gameObject.SetActive(false);
                    break;

                case "Triangle":
                    other.GetComponent<TriangleBehaviours>().life--;
                    gameObject.SetActive(false);
                    break;

                case "Cube":
                    if (other.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
                    {
                        other.GetComponent<CubeBehaviours>().life--;
                    }

                    gameObject.SetActive(false);
                    break;

                case "Triple":
                    other.GetComponent<TripleBallBehaviours>().life--;
                    gameObject.SetActive(false);
                    break;

                case "BulletDestroyer":
                    gameObject.SetActive(false);
                    break;

                case "BlockBullets":
                    GameManager.Instance.AddScore(2);
                    gameObject.SetActive(false);
                    break;

                case "Penta":
                    other.GetComponent<PentaBehaviours>().life--;
                    gameObject.SetActive(false);
                    break;

                case "Boss":
                    other.GetComponent<BossHealth>().life--;
                    gameObject.SetActive(false);
                    break;

                case "Star":
                    other.GetComponent<StarBehaviours>().life--;
                    gameObject.SetActive(false);
                    break;
                
                case "TripleTriangle":
                    other.GetComponent<TripleTriangle>().life--;
                    gameObject.SetActive(false);
                    break;

                default:
                    Debug.Log("j'ai touché");
                    other.GetComponent<EnemyBehaviour>().life--;
                    gameObject.SetActive(false);
                    break;
            }
            
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
