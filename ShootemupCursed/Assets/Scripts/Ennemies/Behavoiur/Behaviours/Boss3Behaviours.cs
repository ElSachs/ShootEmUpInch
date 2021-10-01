using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boss3Behaviours : EnemyBehaviour
{
    [SerializeField] private Rigidbody2D Redlaser;
    [SerializeField] private Rigidbody2D BlueLaser;
    [SerializeField] private float laserSpeed;
    [SerializeField] private Animation glow;
    [SerializeField] private Transform redSpawnPoint;
    [SerializeField] private Transform redSpawnPoint2;
    [SerializeField] private Transform blueSpawnPoint;
    [SerializeField] private Transform blueSpawnPoint2;
    [SerializeField] private PoolManager.Generate blueBullet;
    [SerializeField] private PoolManager.Generate redBullet;
    [SerializeField] private PoolManager.Generate pentaRedBullet;
    [SerializeField] private PoolManager.Generate pentaBlueBullet;
    private PoolManager.Generate bulletToShoot = PoolManager.Generate.RedBullet;
    private float targetY = -9.5f;
    private float targetX = -4f;
    private float timeElapsed;
    private int pattern;
    private bool resetPattern = true;
    private float patternTime;
    private int numberOfShots;
    private bool blueGoToZero;
    private bool redGoToZero;
    private bool blueGoToZero2;
    private bool redGoToZero2;
    [SerializeField] private string animationName;
    public override void Start()
    {
        base.Start();
        timeElapsed = Time.time;
        GameManager.Instance.healthBar.SetActive(true);
        BossHealthBar.Instance.boss = gameObject;
        Debug.Log(BossHealthBar.Instance.boss.name);
    }

    public override void Update()
    {

        if (Time.time >= timeElapsed + timeUntilStop)
        {
            self.velocity = Vector2.zero;
            GameManager.Instance.shootEnable = true;
            WaveManager.Instance.doomOfBullet.SetActive(false);
        }

        if (life <= 0)
        {
            PoolManager.Instance.spawnFromPool(PoolManager.Generate.ExplosionRed, transform);
            PoolManager.Instance.spawnFromPool(PoolManager.Generate.ExplosionBlue, transform);
            GameManager.Instance.AddScore(scoreGive);
            Debug.Log("mort");
            gameObject.SetActive(false);
            Drop();
            waveManager = GameObject.Find("WaveManager").transform;
            WaveManager.enemiesLeft = WaveManager.enemiesLeft - 1;
        }

        if (resetPattern)
        {
            pattern = 0;
            patternTime = 1f;
            resetPattern = false;
        }

        if (patternTime > 0f)
        {
            patternTime -= Time.deltaTime;
        }

        if (patternTime < 0f && GameManager.Instance.shootEnable)
        {
            pattern = Random.Range(1, 4);
            Debug.Log(pattern);
            patternTime = 0f;
        }

        switch (pattern)
        {
            case 1 :
                
                resetPattern = false;
                if (resetShoot)
                {
                    StartCoroutine(ShootingLaser());
                    numberOfShots++;
                }
                if (numberOfShots >= 1)
                {
                    resetPattern = true;
                    resetShoot = false;
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
                if (numberOfShots >= 100)
                {
                    resetPattern = true;
                    resetShoot = false;
                    numberOfShots = 0;
                        
                }
                break;
            case 3 :
                resetPattern = false;
                if (resetShoot)
                {
                    Shooting3();
                    numberOfShots++;
                }
                if (numberOfShots >= 10)
                {
                    resetPattern = true;
                    resetShoot = false;
                    numberOfShots = 0;
                        
                }

                break;
        }
    }

    IEnumerator ShootingLaser()
    {
        shootingRate = 4f;
        resetShoot = false;
        WaveManager.cubeShooting = true;
        glow.Play(animationName);
        yield return new WaitForSeconds(2f);
        Debug.Log("start corout");
        Rigidbody2D shotLaser = Instantiate(Redlaser, new Vector2(4.5f, 37f), Quaternion.identity);
        Rigidbody2D shotLaser2 = Instantiate(BlueLaser, new Vector2(-4.5f, 37f), Quaternion.identity);
        shotLaser.AddForce(Vector3.down * laserSpeed);
        shotLaser2.AddForce(Vector2.down * laserSpeed);
        Invoke(("ResetShoot"), shootingRate);
    }

    private void Shooting2()
    {
        shootingRate = 0.2f;
        resetShoot = false;
        bulletSpeed = 300f;
        if (blueSpawnPoint.position.y >= -5f && blueGoToZero == false)
        {
            blueSpawnPoint.position = new Vector2(blueSpawnPoint.position.x , blueSpawnPoint.position.y - 0.5f);
            
        }
        else if (blueSpawnPoint.position.y >= 3.5f)
        {
            blueGoToZero = false;
        }
        else if (blueSpawnPoint.position.y >= -5f && blueGoToZero)
        {
            blueSpawnPoint.position = new Vector2(blueSpawnPoint.position.x, blueSpawnPoint.position.y + 0.5f);
        }
        else if (blueSpawnPoint.position.y <= -5f)
        {
            blueSpawnPoint.position = new Vector2(blueSpawnPoint.position.x, blueSpawnPoint.position.y + 0.5f);
            blueGoToZero = true;
        }
        
        
        if (redSpawnPoint.position.y >= -5f && redGoToZero == false)
        {
            redSpawnPoint.position = new Vector2(redSpawnPoint.position.x , redSpawnPoint.position.y - 0.5f);
        }
        
        else if (redSpawnPoint.position.y >= 3.5f)
        {
            redGoToZero = false;
        }
        
        else if(redSpawnPoint.position.y >= -5f && redGoToZero)
        {
            redSpawnPoint.position = new Vector2(redSpawnPoint.position.x, redSpawnPoint.position.y + 0.5f);
            
        }
        
        else if (redSpawnPoint.position.y <= -5f)
        {
            redSpawnPoint.position = new Vector2(redSpawnPoint.position.x, redSpawnPoint.position.y + 0.5f);
            redGoToZero = true;
        }
        
        if (blueSpawnPoint2.position.y >= -5f && blueGoToZero2 == false)
        {
            blueSpawnPoint2.position = new Vector2(blueSpawnPoint2.position.x , blueSpawnPoint2.position.y - 0.5f);
            
        }
        else if (blueSpawnPoint2.position.y >= 3.5f)
        {
            blueGoToZero2 = false;
        }
        else if (blueSpawnPoint2.position.y >= -5f && blueGoToZero2)
        {
            blueSpawnPoint2.position = new Vector2(blueSpawnPoint2.position.x, blueSpawnPoint2.position.y + 0.5f);
        }
        else if (blueSpawnPoint2.position.y <= -5f)
        {
            blueSpawnPoint2.position = new Vector2(blueSpawnPoint2.position.x, blueSpawnPoint2.position.y + 0.5f);
            blueGoToZero2 = true;
        }
        
        
        if (redSpawnPoint2.position.y >= -5f && redGoToZero2 == false)
        {
            redSpawnPoint2.position = new Vector2(redSpawnPoint2.position.x , redSpawnPoint2.position.y - 0.5f);
        }
        
        else if (redSpawnPoint2.position.y >= 3.5f)
        {
            redGoToZero2 = false;
        }
        
        else if(redSpawnPoint2.position.y >= -5f && redGoToZero2)
        {
            redSpawnPoint2.position = new Vector2(redSpawnPoint2.position.x, redSpawnPoint2.position.y + 0.5f);
            
        }
        
        else if (redSpawnPoint2.position.y <= -5f)
        {
            redSpawnPoint2.position = new Vector2(redSpawnPoint2.position.x, redSpawnPoint2.position.y + 0.5f);
            redGoToZero2 = true;
        }

        Rigidbody2D blueBulletShot = PoolManager.Instance.spawnFromPool(blueBullet, blueSpawnPoint);
        blueBulletShot.AddForce(Vector2.right * bulletSpeed);
        Rigidbody2D redBulletShot = PoolManager.Instance.spawnFromPool(redBullet, redSpawnPoint);
        redBulletShot.AddForce(Vector2.left * bulletSpeed);
        Rigidbody2D blueBulletShot2 = PoolManager.Instance.spawnFromPool(blueBullet, blueSpawnPoint2);
        blueBulletShot2.AddForce(Vector2.right * bulletSpeed);
        Rigidbody2D redBulletShot2 = PoolManager.Instance.spawnFromPool(redBullet, redSpawnPoint2);
        redBulletShot2.AddForce(Vector2.left * bulletSpeed);
        Invoke("ResetShoot", shootingRate);
    }

     private void Shooting3()
    {
        resetShoot = false;
        bulletSpeed = 200f;
        shootingRate = 0.25f;
        for (int i = 0; i < 18; i++)
        {
            
            Vector3 line = new Vector2(targetX + i/2f, targetY);
            Rigidbody2D bulletShot = PoolManager.Instance.spawnFromPool(bulletToShoot, transform);
            bulletShot.AddForce(line.normalized * bulletSpeed);
            if (bulletToShoot == PoolManager.Generate.RedBullet)
            {
                bulletToShoot = PoolManager.Generate.BlueBullet;
            }
            else
            {
                bulletToShoot = PoolManager.Generate.RedBullet;
            }
            
        }

        bulletSpeed = 75f;
        shootingRate = 2f;
        for (int i = 0; i < 6; i++)
        {
            Vector3 target = new Vector2(Random.Range(-3.5f, 3.5f), Random.Range(-4.5f, 1f));
            Vector3 bossToTarget =  target - transform.position;
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
        Invoke(("ResetShoot"), shootingRate);
    }
}
