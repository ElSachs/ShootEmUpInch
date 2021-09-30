using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Boss2Behaviours : EnemyBehaviour
{
    [SerializeField] private PoolManager.Generate redBullet;
    [SerializeField] private PoolManager.Generate blueBullet;
    [SerializeField] private PoolManager.Generate pentaRedBullet;
    [SerializeField] private PoolManager.Generate pentaBlueBullet;
    [SerializeField] private Transform blueSpawnPoint1;
    [SerializeField] private Transform blueSpawnPoint2;
    [SerializeField] private Transform redSpawnPoint1;
    [SerializeField] private Transform redSpawnPoint2;
    [SerializeField] private GameObject grid;
    [SerializeField] private GameObject preGrid;
    private int pattern;
    private bool isShooting3 = false;
    private bool hasShot3 = false;
    private PoolManager.Generate bulletToShoot;
    private bool resetPattern = true;
    private float timeElapsed;
    [SerializeField] private GameObject healthBar;
    private float patternTime;
    private int numberOfShots;
    private Vector3 target;
    private Vector2 bossToTarget;
    private bool blueGoToZero = false;
    private bool redGoToZero = false;

    public override void Start()
    {
        base.Start();
        timeElapsed = Time.time;
        //pattern = Random.Range(1, 4);
        GameManager.Instance.healthBar.SetActive(true);
        BossHealthBar.Instance.boss = gameObject;
    }

    public override void Update()
    {
        if (Time.time >= timeElapsed + timeUntilStop)
        {
            self.velocity = Vector2.zero;
            GameManager.Instance.shootEnable = true;
            WaveManager.Instance.doomOfBullet.SetActive(false);
        }

        

        if (resetPattern)
        {
            pattern = 0;
            patternTime = 3f;
            resetPattern = false;
        }

        if (patternTime > 0f)
        {
            patternTime -= Time.deltaTime;
        }

        if (patternTime < 0f && GameManager.Instance.shootEnable)
        {
            pattern = Random.Range(1,4);
            patternTime = 0f;
        }

        switch (pattern)
        {
            case 1 :
                resetPattern = false;

                if (resetShoot)
                {
                    Shooting();
                    numberOfShots++;
                }

                if (numberOfShots >= 3)
                {
                    resetPattern = true;
                    resetShoot = false;
                    Debug.Log("k'arrête");
                    numberOfShots = 0;
                }
                break;
            
            case 2 :
                resetPattern = false;

                if (resetShoot)
                {
                    Shooting2();
                    numberOfShots++;
                }

                if (numberOfShots >= 200)
                {
                    resetPattern = true;
                    resetShoot = false;
                    Debug.Log("k'arrête");
                    numberOfShots = 0;
                }

                break;
            
            case 3 :
                resetPattern = false;

                if (!isShooting3)
                {
                    StartCoroutine(Shooting3());
                }

                if (hasShot3)
                {
                    resetPattern = true;
                    resetShoot = false;
                    Debug.Log("k'arrête");
                    numberOfShots = 0;
                    hasShot3 = false;
                }
                break;
        }
        
    }

    public override void Shooting()
    {
        resetShoot = false;
        bulletSpeed = 75f;
        shootingRate = 2f;
        bulletToShoot = pentaBlueBullet;

        for (int i = 0; i < 6; i++)
        {
            target = new Vector2(Random.Range(-3.5f, 3.5f), Random.Range(-4.5f, 1f));
            bossToTarget =  target - transform.position;
            Rigidbody2D shotBullet = PoolManager.Instance.spawnFromPool(bulletToShoot, transform);
            shotBullet.AddForce(bossToTarget.normalized * bulletSpeed);
            Invoke(("ResetShoot"), shootingRate);
            if (bulletToShoot == pentaBlueBullet)
            {
                bulletToShoot = pentaRedBullet;
            }
            else
            {
                bulletToShoot = pentaBlueBullet;
            }
        }
        
        

    }

    private void Shooting2()
    {
        resetShoot = false;
        bulletSpeed = 300f;
        shootingRate = 0.05f;
        if (blueSpawnPoint1.position.x >= -2f && blueGoToZero == false)
        {
            blueSpawnPoint1.position = new Vector2(blueSpawnPoint1.position.x - 0.1f, blueSpawnPoint1.position.y);
            
        }
        else if (blueSpawnPoint1.position.x >= 0f)
        {
            blueGoToZero = false;
        }
        else if (blueSpawnPoint1.position.x >= -2f && blueGoToZero)
        {
            blueSpawnPoint1.position = new Vector2(blueSpawnPoint1.position.x + 0.1f, blueSpawnPoint1.position.y);
        }
        else if (blueSpawnPoint1.position.x <= -2f)
        {
            blueSpawnPoint1.position = new Vector2(blueSpawnPoint1.position.x + 0.1f, blueSpawnPoint1.position.y);
            blueGoToZero = true;
        }
       

        if (redSpawnPoint1.position.x <= 0f && redGoToZero == false)
        {
            redSpawnPoint1.position = new Vector2(redSpawnPoint1.position.x + 0.1f, redSpawnPoint1.position.y);
        }
        
        else if (redSpawnPoint1.position.x <= -2f)
        {
            redGoToZero = false;
        }
        
        else if(redSpawnPoint1.position.x <= 0f && redGoToZero)
        {
            redSpawnPoint1.position = new Vector2(redSpawnPoint1.position.x - 0.1f, redSpawnPoint1.position.y);
            
        }
        
        else if (redSpawnPoint1.position.x >= 0f)
        {
            redSpawnPoint1.position = new Vector2(redSpawnPoint1.position.x - 0.1f, redSpawnPoint1.position.y);
            redGoToZero = true;
        }
        
        
        if (blueSpawnPoint2.position.x <= 2f && redGoToZero == false)
        {
            blueSpawnPoint2.position = new Vector2(blueSpawnPoint2.position.x + 0.1f, blueSpawnPoint2.position.y);
        }
        
        else if (blueSpawnPoint2.position.x <= 0f)
        {
            redGoToZero = false;
        }
        
        else if(blueSpawnPoint2.position.x <= 2f && redGoToZero)
        {
            blueSpawnPoint2.position = new Vector2(blueSpawnPoint2.position.x - 0.1f, blueSpawnPoint2.position.y);
            
        }
        
        else if (blueSpawnPoint2.position.x >= 2f)
        {
            blueSpawnPoint2.position = new Vector2(blueSpawnPoint2.position.x - 0.1f, blueSpawnPoint2.position.y);
            redGoToZero = true;
        }
        
        if (redSpawnPoint2.position.x >= 0f && blueGoToZero == false)
        {
            redSpawnPoint2.position = new Vector2(redSpawnPoint2.position.x - 0.1f, redSpawnPoint2.position.y);
            
        }
        else if (redSpawnPoint2.position.x >= 2f)
        {
            blueGoToZero = false;
        }
        else if (redSpawnPoint2.position.x >= 0f && blueGoToZero)
        {
            redSpawnPoint2.position = new Vector2(redSpawnPoint2.position.x + 0.1f, redSpawnPoint2.position.y);
        }
        else if (redSpawnPoint2.position.x <= 0f)
        {
            redSpawnPoint2.position = new Vector2(redSpawnPoint2.position.x + 0.1f, redSpawnPoint2.position.y);
            blueGoToZero = true;
        }
        
        
        
        Rigidbody2D redBulletShot = PoolManager.Instance.spawnFromPool(redBullet, redSpawnPoint1);
        redBulletShot.AddForce(Vector2.down * bulletSpeed);
        Rigidbody2D blueBulletShot = PoolManager.Instance.spawnFromPool(blueBullet, blueSpawnPoint1);
        blueBulletShot.AddForce(Vector2.down * bulletSpeed);
        Rigidbody2D blueBulletShot2 = PoolManager.Instance.spawnFromPool(blueBullet, blueSpawnPoint2);
        blueBulletShot2.AddForce(Vector2.down * bulletSpeed);
        Rigidbody2D redBulletShot2 = PoolManager.Instance.spawnFromPool(redBullet, redSpawnPoint2);
        redBulletShot2.AddForce(Vector2.down * bulletSpeed);
        
        Invoke(("ResetShoot"), shootingRate);
    }

    IEnumerator Shooting3()
    {
        isShooting3 = true;
        GameObject spawnedPreGrid = Instantiate(preGrid);
        yield return new WaitForSeconds(2f);
        Destroy(spawnedPreGrid);
        StartCoroutine(GridSpawn());

    }

    IEnumerator GridSpawn()
    {
        GameObject newGrid = Instantiate(grid);
        yield return new WaitForSeconds(4f);
        Destroy(newGrid);
        isShooting3 = false;
        hasShot3 = true;
        
    }
}
