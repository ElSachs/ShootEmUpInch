using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(0f, 1f, 0f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("get");
            //other.gameObject.GetComponents<PlayerController>().shootBullet++;
        }
    }
}
