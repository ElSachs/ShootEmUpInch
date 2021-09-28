using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Rigidbody2D RedEnemy;
    [SerializeField] private Rigidbody2D BlueEnemy;
    [SerializeField] private Rigidbody2D RedTriangle;
    [SerializeField] private Rigidbody2D BlueTriangle;
    [SerializeField] private Rigidbody2D BlueCube;
    [SerializeField] private Rigidbody2D RedCube;
    [SerializeField] private Rigidbody2D TripleRed;
    [SerializeField] private Rigidbody2D TripleBlue;
    private int waveType = 0;
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
        waveType ++;
        Debug.Log(waveType);
        switch (waveType)
        {
            case 1 :
                enemyToSpawn = RedEnemy;
                distanceBeetweenEnemiesX = 1f;
                distanceBeetweenEnemiesY = 0f;
                enemiesToSpawn = 8;
                Wave();
                spawnPoint = initialSpawnPoint;
                enemySpawned = 0;
                
                break;
            case 2 :
                enemyToSpawn = BlueEnemy;
                distanceBeetweenEnemiesX = 1f;
                distanceBeetweenEnemiesY = 0f;
                enemiesToSpawn = 8;
                Wave();
                spawnPoint = initialSpawnPoint;
                enemySpawned = 0;
                break;
            case 3 :
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
            case 4 :
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
            case 5 :
                enemyToSpawn = RedTriangle;
                distanceBeetweenEnemiesX = 0f;
                distanceBeetweenEnemiesY = 1f;
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
            case 6 :
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
            
            case 7 :
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
                break;
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
