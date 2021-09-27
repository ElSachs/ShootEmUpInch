using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleBallBehaviour : MonoBehaviour
{
    private Rigidbody2D self;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D bullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float shootingRate;
    public bool resetShoot = true;
    public int life = 5;
    private Transform waveManager;
    private void Start()
    {
        self = GetComponent<Rigidbody2D>();
        self.AddForce(Vector2.down * moveSpeed);
        resetShoot = true;
    }

    private void Update()
    {
        if (resetShoot == true)
        {
            Shooting();
        }

        if (transform.position.y <= 3f)
        {
            self.velocity = Vector2.zero;
        }
        if (life == 0)
        {
            gameObject.SetActive(false);
            waveManager = GameObject.Find("WaveManager").transform;
            WaveManager.enemiesLeft = WaveManager.enemiesLeft - 1;
        }
    }

    void Shooting()
    {
        resetShoot = false;
        Rigidbody2D shotBullet = Instantiate(bullet, transform.position, transform.rotation);
        Rigidbody2D shotBullet2 = Instantiate(bullet, transform.position, Quaternion.identity);
        Rigidbody2D shotBullet3 = Instantiate(bullet, transform.position, Quaternion.identity);
        shotBullet.AddForce(Vector2.down * bulletSpeed);
        shotBullet2.AddForce((Vector2.down - Vector2.left) * bulletSpeed);
        shotBullet3.AddForce((Vector2.down - Vector2.right) * bulletSpeed);
        Invoke(("ResetShoot"), shootingRate);
    }

    private void ResetShoot()
    {
        resetShoot = true;
    }
}