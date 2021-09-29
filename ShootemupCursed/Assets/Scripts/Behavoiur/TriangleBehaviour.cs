using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleBehaviour : MonoBehaviour
{
    [SerializeField] private Transform waveManager;
    [SerializeField] private Rigidbody2D self;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float shootingRate;
    [SerializeField] private Rigidbody2D bullet;
    private bool resetShoot = true;
    private Vector2 triangleToPlayer;
    [SerializeField] private Transform player;
    public int life = 4;
    public PoolManager.Generate typeOfBullet;
    private float timeElapsed;
    private float timeUntilStop = 4f;
    [SerializeField] private int scoreGive = 30;


    private void Start()
    {
        waveManager = GameObject.Find("WaveManager").transform;
        player = GameObject.Find("Player").transform;
        self = GetComponent<Rigidbody2D>();
        self.AddForce(Vector2.down * moveSpeed);
        timeElapsed = Time.time;
    }

    private void Update()
    {
        
        if (resetShoot)
        {
            Shooting();
        }
        LookAtPlayer();
        if (life <= 0)
        {
            GameManager.Instance.AddScore(scoreGive);
            gameObject.SetActive(false);
            waveManager = GameObject.Find("WaveManager").transform;
            WaveManager.enemiesLeft = WaveManager.enemiesLeft - 1;
        }

        if (Time.time > timeElapsed + timeUntilStop)
        {
            self.velocity = Vector2.zero;
        }
    }
    void Shooting()
    {
        resetShoot = false;
        Rigidbody2D shotBullet = PoolManager.Instance.spawnFromPool(typeOfBullet, transform);
        shotBullet.AddForce(triangleToPlayer * bulletSpeed);
        Invoke(("ResetShoot"), shootingRate);
    }

    private void ResetShoot()
    {
        resetShoot = true;
    }
    
    private void LookAtPlayer()
    {
        triangleToPlayer = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y ).normalized;
        float angle = Mathf.Atan2(triangleToPlayer.y, triangleToPlayer.x) * Mathf.Rad2Deg;
        self.rotation = angle + 90f;
    }
}
