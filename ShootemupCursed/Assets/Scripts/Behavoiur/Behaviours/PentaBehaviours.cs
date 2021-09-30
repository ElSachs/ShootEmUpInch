using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PentaBehaviours : EnemyBehaviour
{
    private Vector3 target;
    private Vector3 pentaToTarget;
    private float timeElapsed;

    public override void Start()
    {
        base.Start();
        timeElapsed = Time.time;
    }

    public override void Update()
    {
        base.Update();
        if (Time.time >= timeElapsed + timeUntilStop)
        {
            self.velocity = Vector2.zero; 
        }
    }

    public override void Shooting()
    {
        resetShoot = false;
        target = new Vector2(Random.Range(-3.5f, 3.5f), Random.Range(-4.5f, 1f));
        pentaToTarget = target - transform.position;
        Rigidbody2D shotBullet = PoolManager.Instance.spawnFromPool(bullet, transform);
        shotBullet.AddForce(pentaToTarget.normalized * bulletSpeed);
        Invoke(("ResetShoot"), shootingRate);
    }
}
