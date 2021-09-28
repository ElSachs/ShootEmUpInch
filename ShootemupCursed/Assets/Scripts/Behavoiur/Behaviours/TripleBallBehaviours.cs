using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleBallBehaviours : EnemyBehaviour
{
    public override void Shooting()
    {
        resetShoot = false;
        Rigidbody2D shotBullet = PoolManager.Instance.spawnFromPool(bullet, transform);
        Rigidbody2D shotBullet2 = PoolManager.Instance.spawnFromPool(bullet, transform);
        Rigidbody2D shotBullet3 = PoolManager.Instance.spawnFromPool(bullet, transform);
        shotBullet.AddForce(Vector2.down * bulletSpeed);
        shotBullet2.AddForce((Vector2.down - Vector2.left) * bulletSpeed);
        shotBullet3.AddForce((Vector2.down - Vector2.right) * bulletSpeed);
        Invoke(("ResetShoot"), shootingRate);
    }
}
