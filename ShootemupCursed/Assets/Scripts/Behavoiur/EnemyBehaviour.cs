using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Rigidbody2D self;
    [SerializeField] public float moveSpeed;
    [SerializeField] public PoolManager.Generate bullet;
    [SerializeField] public float bulletSpeed;
    [SerializeField] public float shootingRate;
    [SerializeField] public int scoreGive = 30;
    [SerializeField] public float stoppingPoint = 2.5f;
    public int life = 3;
    public bool resetShoot;
    public Transform waveManager;
    [SerializeField] private Loot[] loots;
    
    [System.Serializable]
    public class Loot
    {
        public PoolManager.Generate bonusType;
        public int chanceOfDrop;
    }
    
    public virtual void Start()
    {
        waveManager = GameObject.Find("WaveManager").transform;
            
        self = GetComponent<Rigidbody2D>();
        self.AddForce(Vector2.down * moveSpeed);
        resetShoot = true;
    }

    public virtual void Update()
    {
        if(resetShoot == true)
            Shooting();
        if (life <= 0)
        {
            GameManager.Instance.AddScore(scoreGive);
            //Debug.Log("mort");
            Drop();
            gameObject.SetActive(false);
            WaveManager.enemiesLeft = WaveManager.enemiesLeft - 1;
        }

        if (transform.position.y <= stoppingPoint)
        {
            self.velocity = Vector2.zero; 
        }
    }

    public virtual void Shooting()
    {
        resetShoot = false;
        Rigidbody2D shotBullet = PoolManager.Instance.spawnFromPool(bullet, transform);
        shotBullet.AddForce(Vector2.down * bulletSpeed);
        Invoke(("ResetShoot"), shootingRate);
    }

    public void Drop()
    {
        foreach (Loot loot in loots)
        {
            float intt = UnityEngine.Random.Range(1f, 100f);
            //Debug.Log("intt : " + intt);
            
            if (intt <= loot.chanceOfDrop)
            {
                Debug.Log("drop");
                PoolManager.Instance.spawnFromPool(loot.bonusType, transform);
            }
        }
    }
    
    private void ResetShoot()
    {
        resetShoot = true;
    }
}
