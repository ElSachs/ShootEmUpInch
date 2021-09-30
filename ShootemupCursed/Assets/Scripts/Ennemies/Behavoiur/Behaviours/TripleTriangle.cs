using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleTriangle : TriangleBehaviours
{
    [SerializeField] private Transform spawnPoint1;
    [SerializeField] private Transform spawnPoint2;
    [SerializeField] private Transform spawnPoint3;

    public override void Shooting()
    {
        resetShoot = false;
        Rigidbody2D shotBullet1 = PoolManager.Instance.spawnFromPool(bullet, spawnPoint1);
        Vector3 target1 = player.position - spawnPoint1.position;
        shotBullet1.AddForce(target1.normalized * bulletSpeed);
        
        Rigidbody2D shotBullet2 = PoolManager.Instance.spawnFromPool(bullet, spawnPoint2);
        Vector3 target2 = player.position - spawnPoint2.position;
        shotBullet2.AddForce(target2.normalized * bulletSpeed);
        
        Rigidbody2D shotBullet3 = PoolManager.Instance.spawnFromPool(bullet, spawnPoint3);
        Vector3 target3 = player.position - spawnPoint3.position;
        shotBullet3.AddForce(target3.normalized * bulletSpeed);
        
        Invoke(("ResetShoot"), shootingRate);
    }
}
