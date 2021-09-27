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
    private Vector2 spawnPoint = new Vector2(-3f, 6f);
    private Vector2 initialSpawnPoint;
    [SerializeField] Transform parent;
    private bool waveFinished;
    
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
        int waveType = Random.Range(1, 3);
        Debug.Log(waveType);
        switch (waveType)
        {
            case 1 :
                initialSpawnPoint = spawnPoint;
                for (int i = 0; i < 7; i++)
                {
                    Rigidbody2D spawnedEnemy = Instantiate(RedEnemy, spawnPoint, Quaternion.identity);
                    spawnPoint.x = spawnPoint.x + 1f;
                    spawnedEnemy.transform.SetParent(parent);
                }
                spawnPoint = initialSpawnPoint;
                break;
            case 2 :
                initialSpawnPoint = spawnPoint;
                for (int i = 0; i < 7; i++)
                {
                    Rigidbody2D spawnedEnemy = Instantiate(BlueEnemy, spawnPoint, Quaternion.identity);
                    spawnPoint.x = spawnPoint.x + 1f;
                    spawnedEnemy.transform.SetParent(parent);
                }

                spawnPoint = initialSpawnPoint;
                break;
        }
        
    }
}
