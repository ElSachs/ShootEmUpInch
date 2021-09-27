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
            gameObject.SetActive(false);
            waveManager = GameObject.Find("WaveManager").transform;
            WaveManager.enemiesToSpawn = WaveManager.enemiesToSpawn - 1;
        }
    }

    void Shooting()
    {
        resetShoot = false;
        Debug.Log("wsh");
        PoolManager.Instance.spawnFromPool(bullet, transform);
        Invoke(("ResetShoot"), shootingRate);
    }

    private void ResetShoot()
    {
        resetShoot = true;
    }
}
