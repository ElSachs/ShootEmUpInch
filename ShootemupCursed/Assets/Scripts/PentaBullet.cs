using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PentaBullet : MonoBehaviour
{
    private float timeElapsed;
    private float timeUntilStop = 3f;
    [SerializeField] private Rigidbody2D rb;
    private bool destroying;
    [SerializeField] float bulletSpeed;
    [SerializeField] private PoolManager.Generate bullet;

    void Start()
    {
        timeElapsed = Time.time;
        rb = GetComponent<Rigidbody2D>();
        destroying = false;
    }


    void Update()
    {
        if (Time.time >= timeElapsed + timeUntilStop)
        {
            rb.velocity = Vector2.zero;
            
            if(destroying == false)
                StartCoroutine(Shooting());
        }
    }

    IEnumerator Shooting()
    {
        destroying = true;
        Debug.Log("jv exploser");
        yield return new WaitForSeconds(1f);
        Vector3 shootingDirection = Vector3.right;
        for (int i = 0; i < 18; i++)
        {
            shootingDirection = Quaternion.AngleAxis(20f, Vector3.forward) * shootingDirection;
            Rigidbody2D shotBullet = PoolManager.Instance.spawnFromPool(bullet, transform);
            shotBullet.AddForce(shootingDirection.normalized * bulletSpeed);
            Destroy(gameObject);
            
        }
        
        
    }
}
