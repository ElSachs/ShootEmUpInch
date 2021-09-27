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
    private int waveType;
    private Vector2 spawnPoint = new Vector2(-3.5f, 6f);
    private Vector2 initialSpawnPoint;
    [SerializeField] Transform parent;
    private bool waveFinished;
    private Rigidbody2D enemyToSpawn;
    private int enemySpawned;
    
    private void Start()
    {

    }

    private void Update()
    {
        if (waveFinished)
        {
            waveSpawning();
        }
        if(parent.GetChildCount() == 0f)
        {
            waveFinished = true;
        }
    }

    void waveSpawning()
    {
        waveFinished = false;
        int waveType = 3;
        Debug.Log(waveType);
        switch (waveType)
        {
            case 1 :
                enemyToSpawn = BlueEnemy;
                Wave();
                
                break;
            case 2 :
                enemyToSpawn = RedEnemy;
                Wave();
                break;
            case 3 :
                enemyToSpawn = RedEnemy;
                
                Wave();
                if (enemySpawned == 4)
                {
                    enemyToSpawn = BlueEnemy;
                }
                break;
                
        }
        
    }

    void Wave()
    {
        initialSpawnPoint = spawnPoint;
        enemySpawned = 0;
        for (int i = 0; i < 8; i++)
        {
            Rigidbody2D spawnedEnemy = Instantiate(enemyToSpawn, spawnPoint, Quaternion.identity);
            spawnPoint.x = spawnPoint.x + 1f;
            spawnedEnemy.transform.SetParent(parent);
            enemySpawned = enemySpawned + 1;
        }

        spawnPoint = initialSpawnPoint;
        

    }
}
