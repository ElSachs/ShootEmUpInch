using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEditor.Experimental;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    [SerializeField] private Rigidbody2D RedEnemy;
    [SerializeField] private Rigidbody2D BlueEnemy;
    [SerializeField] private Rigidbody2D RedTriangle;
    [SerializeField] private Rigidbody2D BlueTriangle;
    [SerializeField] private Rigidbody2D BlueCube;
    [SerializeField] private Rigidbody2D RedCube;
    [SerializeField] private Rigidbody2D TripleRed;
    [SerializeField] private Rigidbody2D TripleBlue;
    [SerializeField] private Rigidbody2D PentaBlue;
    [SerializeField] private Rigidbody2D PentaRed;
    [SerializeField] private Rigidbody2D RedStar;
    [SerializeField] private Rigidbody2D BlueStar;
    [SerializeField] private Rigidbody2D Boss;
    [SerializeField] private Rigidbody2D Boss2;


    public int waveType = 0;
    public int level = 1;

    private Vector2 spawnPoint = new Vector2(-3.5f, 6f);
    private static Vector2 initialSpawnPoint = new Vector2(-3.5f, 6f);
    [SerializeField] Transform parent;
    private bool waveFinished;
    private Rigidbody2D enemyToSpawn;
    private int enemySpawned;
    private float distanceBeetweenEnemiesX;
    private float distanceBeetweenEnemiesY;
    private int enemiesToSpawn = 0;
    public static bool cubeShooting = false;
    public static int enemiesLeft = 0;

    private void Awake()
    {
        Instance = this;
        waveType --;
        waveFinished = true;
        enemiesLeft = 0;
    }

    private void Update()
    {
        if (waveFinished)
        {
            waveSpawning();
        }
        if(enemiesLeft == 0f)
        {
            waveFinished = true;
        }
    }

    void waveSpawning()
    {
        waveFinished = false;
        if(waveType == 10)
        {
            SoundController.Instance.NextLevel();
        }
        waveType ++;

        if (level == 1)
        {
            switch (waveType)
            {
                case 1:
                    enemyToSpawn = RedEnemy;
                    distanceBeetweenEnemiesX = 1f;
                    distanceBeetweenEnemiesY = 0f;
                    enemiesToSpawn = 8;
                    Wave();
                    spawnPoint = initialSpawnPoint;
                    enemySpawned = 0;

                    break;
                case 2:
                    enemyToSpawn = BlueEnemy;
                    distanceBeetweenEnemiesX = 1f;
                    distanceBeetweenEnemiesY = 0f;
                    enemiesToSpawn = 8;
                    Wave();
                    spawnPoint = initialSpawnPoint;
                    enemySpawned = 0;
                    break;
                case 3:
                    enemyToSpawn = RedEnemy;
                    distanceBeetweenEnemiesX = 1f;
                    distanceBeetweenEnemiesY = 0f;
                    enemiesToSpawn = 8;
                    enemySpawned = 0;
                    for (int i = 0; i < enemiesToSpawn; i++)
                    {
                        Rigidbody2D spawnedEnemy = Instantiate(enemyToSpawn, spawnPoint, Quaternion.identity);
                        spawnPoint.x = spawnPoint.x + 1f;
                        spawnedEnemy.transform.SetParent(parent);
                        enemySpawned = enemySpawned + 1;
                        enemiesLeft++;
                        if (enemySpawned == 4)
                        {
                            enemyToSpawn = BlueEnemy;
                        }
                    }

                    spawnPoint = initialSpawnPoint;
                    enemySpawned = 0;
                    break;
                case 4:
                    enemyToSpawn = RedEnemy;
                    distanceBeetweenEnemiesX = 2f;
                    distanceBeetweenEnemiesY = 0f;
                    enemiesToSpawn = 4;
                    Wave();
                    enemyToSpawn = BlueEnemy;
                    enemiesToSpawn = 4;
                    spawnPoint = new Vector2(initialSpawnPoint.x + 1, initialSpawnPoint.y);
                    Wave();
                    spawnPoint = initialSpawnPoint;
                    enemySpawned = 0;
                    break;
                case 5:
                    enemyToSpawn = RedTriangle;
                    distanceBeetweenEnemiesX = 0f;
                    distanceBeetweenEnemiesY = -1f;
                    enemiesToSpawn = 3;
                    Wave();
                    enemyToSpawn = BlueTriangle;
                    spawnPoint.x = 3.5f;
                    spawnPoint.y = initialSpawnPoint.y;
                    enemiesToSpawn = 3;
                    Wave();
                    initialSpawnPoint = new Vector2(-3.5f, 6f);
                    spawnPoint = initialSpawnPoint;
                    enemySpawned = 0;

                    break;
                case 6:
                    enemyToSpawn = RedCube;
                    distanceBeetweenEnemiesX = 0f;
                    distanceBeetweenEnemiesY = 0f;
                    enemiesToSpawn = 1;
                    Wave();
                    enemyToSpawn = BlueCube;
                    spawnPoint.x = 3.5f;
                    Wave();
                    spawnPoint = initialSpawnPoint;
                    enemySpawned = 0;
                    break;
                case 7:
                    enemyToSpawn = TripleBlue;
                    enemySpawned = 0;
                    distanceBeetweenEnemiesX = 1f;
                    distanceBeetweenEnemiesY = 0f;
                    enemiesToSpawn = 8;
                    for (int i = 0; i < enemiesToSpawn; i++)
                    {
                        Rigidbody2D spawnedEnemy = Instantiate(enemyToSpawn, spawnPoint, Quaternion.identity);
                        spawnPoint.x = spawnPoint.x + 1f;
                        spawnedEnemy.transform.SetParent(parent);
                        enemySpawned = enemySpawned + 1;
                        enemiesLeft++;
                        if (enemySpawned == 4)
                        {
                            enemyToSpawn = TripleRed;
                        }
                    }

                    Debug.Log(enemiesLeft);
                    enemySpawned = 0;
                    spawnPoint = initialSpawnPoint;
                    break;
                case 8:
                    enemyToSpawn = RedTriangle;
                    distanceBeetweenEnemiesX = 1f;
                    distanceBeetweenEnemiesY = 0f;
                    enemiesToSpawn = 8;
                    enemySpawned = 0;
                    for (int i = 0; i < enemiesToSpawn; i++)
                    {
                        Rigidbody2D spawnedEnemy = Instantiate(enemyToSpawn, spawnPoint, Quaternion.identity);
                        spawnPoint.x = spawnPoint.x + 1f;
                        spawnedEnemy.transform.SetParent(parent);
                        enemySpawned = enemySpawned + 1;
                        enemiesLeft++;
                        if (enemySpawned == 4)
                        {
                            enemyToSpawn = BlueTriangle;
                        }
                    }

                    enemyToSpawn = TripleRed;
                    spawnPoint = new Vector2(initialSpawnPoint.x, initialSpawnPoint.y - 1f);
                    enemySpawned = 0;
                    for (int i = 0; i < enemiesToSpawn; i++)
                    {
                        Rigidbody2D spawnedEnemy = Instantiate(enemyToSpawn, spawnPoint, Quaternion.identity);
                        spawnPoint.x = spawnPoint.x + 1f;
                        spawnedEnemy.transform.SetParent(parent);
                        enemySpawned = enemySpawned + 1;
                        enemiesLeft++;
                        if (enemySpawned == 4)
                        {
                            enemyToSpawn = TripleBlue;
                        }
                    }

                    enemyToSpawn = RedEnemy;
                    spawnPoint = new Vector2(initialSpawnPoint.x, initialSpawnPoint.y - 1.5f);
                    enemySpawned = 0;
                    for (int i = 0; i < enemiesToSpawn; i++)
                    {
                        Rigidbody2D spawnedEnemy = Instantiate(enemyToSpawn, spawnPoint, Quaternion.identity);
                        spawnPoint.x = spawnPoint.x + 1f;
                        spawnedEnemy.transform.SetParent(parent);
                        enemySpawned = enemySpawned + 1;
                        enemiesLeft++;
                        if (enemySpawned == 4)
                        {
                            enemyToSpawn = BlueEnemy;
                        }
                    }

                    break;
                case 9:
                    enemyToSpawn = PentaBlue;
                    enemySpawned = 0;
                    enemiesToSpawn = 2;
                    distanceBeetweenEnemiesX = 1f;
                    spawnPoint = initialSpawnPoint;
                    Wave();
                    enemiesToSpawn = 1;
                    spawnPoint = new Vector2(-3f, 5f);
                    Wave();
                    enemyToSpawn = PentaRed;
                    enemySpawned = 0;
                    enemiesToSpawn = 2;
                    distanceBeetweenEnemiesX = 1f;
                    spawnPoint = new Vector2(2.5f, 6f);
                    Wave();
                    enemiesToSpawn = 1;
                    spawnPoint = new Vector2(3f, 5f);
                    Wave();
                    spawnPoint = initialSpawnPoint;
                    enemySpawned = 0;
                    break;
                case 10:
                    spawnPoint = new Vector2(0f, initialSpawnPoint.y);
                    enemyToSpawn = Boss;
                    enemiesToSpawn = 1;
                    Wave();
                    spawnPoint = initialSpawnPoint;
                    enemySpawned = 0;
                    break;
                    

            }
        }

        if (level == 2)
        {
            switch (waveType)
            {
                case 1 :

                    for (int i = 0; i < 2; i++)
                    {
                        enemyToSpawn = RedEnemy;
                        distanceBeetweenEnemiesX = 2f;
                        distanceBeetweenEnemiesY = 0f;
                        enemiesToSpawn = 4;
                        Wave();
                        enemyToSpawn = BlueEnemy;
                        enemiesToSpawn = 4;
                        spawnPoint = new Vector2(initialSpawnPoint.x + 1, spawnPoint.y);
                        Wave(); 
                        enemySpawned = 0;
                        spawnPoint = new Vector2(initialSpawnPoint.x, spawnPoint.y - 1f);
                    }
                    spawnPoint = initialSpawnPoint;
                    enemySpawned = 0;
                    
                    break;
                
                case 2 :
                    enemyToSpawn = TripleBlue;
                    enemiesToSpawn = 2;
                    distanceBeetweenEnemiesX = 4f;
                    spawnPoint = new Vector2(spawnPoint.x + 1.5f, initialSpawnPoint.y);
                    Wave();
                    spawnPoint = new Vector2(initialSpawnPoint.x + 2f, initialSpawnPoint.y - 1f);
                    distanceBeetweenEnemiesX = 3f;
                    Wave();
                    enemiesToSpawn = 1;
                    enemyToSpawn = RedCube;
                    spawnPoint = new Vector2(0f, initialSpawnPoint.y);
                    Wave();
                    enemySpawned = 0;
                    spawnPoint = initialSpawnPoint;
                    break;
                
                case 3 :
                    enemyToSpawn = RedStar;
                    enemiesToSpawn = 1;
                    spawnPoint = new Vector2(0f, initialSpawnPoint.y);
                    Wave();
                    spawnPoint = new Vector2(0f, initialSpawnPoint.y - 3f);
                    enemyToSpawn = BlueStar;
                    Wave();
                    enemySpawned = 0;
                    spawnPoint = initialSpawnPoint;
                    break;
                
                case 4 :
                    enemyToSpawn = TripleRed;
                    enemiesToSpawn = 1;
                    distanceBeetweenEnemiesX = 0f;
                    spawnPoint = new Vector2(initialSpawnPoint.x + 0.5f, initialSpawnPoint.y);
                    Wave();
                    enemyToSpawn = TripleBlue;
                    spawnPoint = new Vector2(3f, initialSpawnPoint.y);
                    Wave();
                    distanceBeetweenEnemiesX = 4f;
                    spawnPoint = new Vector2(initialSpawnPoint.x + 1.5f, initialSpawnPoint.y - 0.5f);
                    enemiesToSpawn = 2;
                    enemyToSpawn = RedTriangle;
                    Wave();
                    spawnPoint = new Vector2(0f, spawnPoint.y);
                    enemiesToSpawn = 1;
                    enemyToSpawn = BlueTriangle;
                    Wave();
                    spawnPoint = new Vector2(initialSpawnPoint.x, spawnPoint.y - 1.5f);
                    enemyToSpawn = RedEnemy;
                    distanceBeetweenEnemiesX = 2f;
                    distanceBeetweenEnemiesY = 0f;
                    enemiesToSpawn = 4;
                    Wave();
                    enemyToSpawn = BlueEnemy;
                    enemiesToSpawn = 4;
                    spawnPoint = new Vector2(initialSpawnPoint.x + 1, spawnPoint.y);
                    Wave();
                    spawnPoint = initialSpawnPoint;
                    enemySpawned = 0;
                    break;
                
                case 5 :
                    enemyToSpawn = PentaBlue;
                    spawnPoint = new Vector2(initialSpawnPoint.x + 0.75f, initialSpawnPoint.y);
                    distanceBeetweenEnemiesX = 4f;
                    enemiesToSpawn = 2;
                    Wave();
                    enemyToSpawn = PentaRed;
                    spawnPoint = new Vector2(initialSpawnPoint.x + 2.25f, initialSpawnPoint.y);
                    Wave();
                    spawnPoint = new Vector2(initialSpawnPoint.x + 0.75f, initialSpawnPoint.y - 2f);
                    Wave();
                    enemyToSpawn = PentaBlue;
                    spawnPoint = new Vector2(initialSpawnPoint.x + 2.25f, initialSpawnPoint.y - 2f);
                    Wave();
                    enemySpawned = 0;
                    spawnPoint = initialSpawnPoint;
                    break;
                
                case 6 :
                    spawnPoint = new Vector2(0f, initialSpawnPoint.y - 3f);
                    enemyToSpawn = RedStar;
                    enemiesToSpawn = 1;
                    Wave();
                    distanceBeetweenEnemiesX = 2f;
                    spawnPoint = new Vector2(-2f, initialSpawnPoint.y);
                    enemyToSpawn = BlueTriangle;
                    enemiesToSpawn = 3;
                    Wave();
                    distanceBeetweenEnemiesX = 2.5f;
                    spawnPoint = new Vector2(-2.5f, initialSpawnPoint.y - 1f);
                    enemyToSpawn = RedTriangle;
                    enemiesToSpawn = 3;
                    Wave();
                    distanceBeetweenEnemiesX = 2.5f;
                    spawnPoint = new Vector2(-1.25f, spawnPoint.y);
                    enemyToSpawn = BlueTriangle;
                    enemiesToSpawn = 2;
                    Wave();
                    spawnPoint = initialSpawnPoint;
                    enemySpawned = 0;
                    break;
                
                case 7 :
                    distanceBeetweenEnemiesX = 4f;
                    spawnPoint = new Vector2(initialSpawnPoint.x + 1.5f, initialSpawnPoint.y);
                    enemiesToSpawn = 2;
                    enemyToSpawn = BlueEnemy;
                    Wave();
                    spawnPoint = new Vector2(0f, initialSpawnPoint.y);
                    enemiesToSpawn = 1;
                    enemyToSpawn = RedEnemy;
                    Wave();
                    distanceBeetweenEnemiesX = 4f;
                    spawnPoint = new Vector2(initialSpawnPoint.x + 1.5f, initialSpawnPoint.y - 3f);
                    enemiesToSpawn = 2;
                    enemyToSpawn = RedStar;
                    Wave();
                    distanceBeetweenEnemiesX = 0f;
                    spawnPoint = new Vector2(0f, initialSpawnPoint.y - 3f);
                    distanceBeetweenEnemiesY = 2f;
                    enemiesToSpawn = 2;
                    enemyToSpawn = BlueStar;
                    Wave();
                    spawnPoint = initialSpawnPoint;
                    enemySpawned = 0;
                    
                    break;
                
                case 8 :
                    enemyToSpawn = RedCube;
                    distanceBeetweenEnemiesX = 0f;
                    distanceBeetweenEnemiesY = 0f;
                    enemiesToSpawn = 1;
                    Wave();
                    enemyToSpawn = BlueCube;
                    spawnPoint.x = 3.5f;
                    Wave();
                    spawnPoint.x = 0f;
                    enemyToSpawn = PentaBlue;
                    Wave();
                    spawnPoint.y = initialSpawnPoint.y - 2f;
                    enemyToSpawn = PentaRed;
                    Wave();
                    spawnPoint = initialSpawnPoint;
                    enemySpawned = 0;
                    break;
                
                case 9 :
                    enemyToSpawn = RedTriangle;
                    spawnPoint.x = spawnPoint.x + 1;
                    distanceBeetweenEnemiesX = 1f;
                    distanceBeetweenEnemiesY = 1f;
                    enemiesToSpawn = 2;
                    Wave();
                    spawnPoint = initialSpawnPoint;
                    spawnPoint.x = spawnPoint.x + 1;
                    enemyToSpawn = TripleBlue;
                    distanceBeetweenEnemiesX = -1f;
                    spawnPoint.x = spawnPoint.x + 1f;
                    Wave();
                    enemyToSpawn = BlueTriangle;
                    spawnPoint.x = 2.5f;
                    spawnPoint.y = initialSpawnPoint.y;
                    distanceBeetweenEnemiesX = -1f;
                    distanceBeetweenEnemiesY = 1f;
                    enemiesToSpawn = 2;
                    Wave();
                    spawnPoint = initialSpawnPoint;
                    spawnPoint.x = 1.5f;
                    enemyToSpawn = TripleRed;
                    distanceBeetweenEnemiesX = 1f;
                    Wave();
                    spawnPoint = new Vector2(initialSpawnPoint.x + 1.5f, initialSpawnPoint.y - 3f);
                    enemyToSpawn = RedStar;
                    enemiesToSpawn = 1;
                    Wave();
                    spawnPoint = new Vector2(initialSpawnPoint.x + 5.5f, initialSpawnPoint.y - 3f);
                    enemyToSpawn = BlueStar;
                    Wave();
                    spawnPoint = initialSpawnPoint;
                    enemySpawned = 0;
                    
                    break;
                
                case 10 :
                    spawnPoint = new Vector2(0f, initialSpawnPoint.y);
                    enemyToSpawn = Boss2;
                    enemiesToSpawn = 1;
                    Wave();
                    spawnPoint = initialSpawnPoint;
                    enemySpawned = 0;
                    break;
                    
                
                case 11 :
                    
                    break;
            }
        }

    }

    void Wave()
    {
        enemySpawned = 0;
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Rigidbody2D spawnedEnemy = Instantiate(enemyToSpawn, spawnPoint, Quaternion.identity);
            spawnPoint.x = spawnPoint.x + distanceBeetweenEnemiesX;
            spawnPoint.y = spawnPoint.y + distanceBeetweenEnemiesY;
            spawnedEnemy.transform.SetParent(parent);
            enemySpawned = enemySpawned + 1;
            enemiesLeft++;
        }
    }
}
