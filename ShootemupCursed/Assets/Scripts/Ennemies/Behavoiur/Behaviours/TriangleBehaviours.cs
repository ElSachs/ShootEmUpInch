using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleBehaviours : EnemyBehaviour
{
    public Vector2 triangleToPlayer;
    private float timeElapsed;
    [SerializeField] public Transform player;
    
    public override void Start()
    {
        waveManager = GameObject.Find("WaveManager").transform;
        player = GameObject.Find("Player").transform;
        self = GetComponent<Rigidbody2D>();
        self.AddForce(Vector2.down * moveSpeed);
        timeElapsed = Time.time;
    }

    public override void Update()
    {
        if (resetShoot)
        {
            Shooting();
        }
        LookAtPlayer();
        if (life <= 0)
        {
            if (gameObject.layer == 10) PoolManager.Instance.spawnFromPool(PoolManager.Generate.ExplosionRed, transform);
            else PoolManager.Instance.spawnFromPool(PoolManager.Generate.ExplosionBlue, transform);
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

    public override void Shooting()
    {
        resetShoot = false;
        Rigidbody2D shotBullet = PoolManager.Instance.spawnFromPool(bullet, transform);
        shotBullet.AddForce(triangleToPlayer * bulletSpeed);
        Invoke(("ResetShoot"), shootingRate);
    }
    
    public void LookAtPlayer()
    {
        triangleToPlayer = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y ).normalized;
        float angle = Mathf.Atan2(triangleToPlayer.y, triangleToPlayer.x) * Mathf.Rad2Deg;
        self.rotation = angle + 90f;
    }
}
