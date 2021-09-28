using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private Rigidbody2D self;
    [SerializeField] private float moveSpeed;
    [SerializeField] private PoolManager.Generate bullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float shootingRate;
    public int life = 3;
    public bool resetShoot;
    private Transform waveManager;
    private void Start()
    {
        self = GetComponent<Rigidbody2D>();
        self.AddForce(Vector2.down * moveSpeed);
        resetShoot = true;
    }

    private void Update()
    {
        if(resetShoot == true)
            Shooting();
        if (life == 0)
        {
            Debug.Log("mort");
            gameObject.SetActive(false);
            waveManager = GameObject.Find("WaveManager").transform;
            WaveManager.enemiesLeft = WaveManager.enemiesLeft - 1;
        }

        if (transform.position.y <= 2.5f)
        {
            self.velocity = Vector2.zero; 
        }
    }

    void Shooting()
    {
        resetShoot = false;
        Rigidbody2D shotBullet = PoolManager.Instance.spawnFromPool(bullet, transform);
        shotBullet.AddForce(Vector2.down * bulletSpeed);
        Invoke(("ResetShoot"), shootingRate);
    }

    private void ResetShoot()
    {
        resetShoot = true;
    }
}
