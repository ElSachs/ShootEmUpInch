using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoolManager : MonoBehaviour
{
    #region Singletone

    public static PoolManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    #endregion
    
    [System.Serializable]
    public class Bullet
    {
        public string name;
        public Generate typeOfObject;
        public GameObject Prefabs;
        public int initialSize;
        public BulletScriptable Stat;
    }

    public enum Generate
    {
        ______ShipBullet______, normalBullet,
        ______EnnemieBullet______, RedBullet, BlueBullet, TriangleBlueBullet, TriangleRedBullet,PentaRedBullet,PentaBlueBullet,
        ______Ennemies_______, RedEnnemy, BlueEnemy,
        _____Items______, shootBullet
    }
    
    public Transform Spawnpoint;
    private Dictionary<Generate, Queue<GameObject>> dictionaryPool;
    public Bullet[] pools;

    private void Start()
    {
        dictionaryPool = new Dictionary<Generate, Queue<GameObject>>();
        foreach (Bullet bul in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < bul.initialSize; i++)
            {
                GameObject obj = Instantiate(bul.Prefabs, transform);
                obj.SetActive(false);
                obj.name = bul.name;
                objectPool.Enqueue(obj);
                if (obj.GetComponent<BulletScript>() != null) obj.GetComponent<BulletScript>().stat = bul.Stat;
            }
            dictionaryPool.Add(bul.typeOfObject, objectPool);   
        }
    }

    public Rigidbody2D spawnFromPool(Generate tag, Transform transform)
    {
        GameObject obj = dictionaryPool[tag].Dequeue();
        obj.SetActive(true);
        obj.transform.position = transform.position;
        dictionaryPool[tag].Enqueue(obj);
        return obj.GetComponent<Rigidbody2D>();


    }
}
