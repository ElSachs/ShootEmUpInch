using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleBallBehaviour : MonoBehaviour
{
    private Rigidbody2D self;
    [SerializeField] private float moveSpeed;
    [SerializeField] private PoolManager.Generate bullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float shootingRate;
    [SerializeField] private int scoreGive = 30;
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
        if (life <= 0)
        {
            if (gameObject.layer == 10) PoolManager.Instance.spawnFromPool(PoolManager.Generate.ExplosionRed, transform);
            else PoolManager.Instance.spawnFromPool(PoolManager.Generate.ExplosionBlue, transform);
            GameManager.Instance.AddScore(scoreGive);
            gameObject.SetActive(false);
            waveManager = GameObject.Find("WaveManager").transform;
            WaveManager.enemiesLeft = WaveManager.enemiesLeft - 1;
        }
    }

    void Shooting()
    {
        resetShoot = false;
        Rigidbody2D shotBullet = PoolManager.Instance.spawnFromPool(bullet, transform);
        Rigidbody2D shotBullet2 = PoolManager.Instance.spawnFromPool(bullet, transform);
        Rigidbody2D shotBullet3 = PoolManager.Instance.spawnFromPool(bullet, transform);
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
