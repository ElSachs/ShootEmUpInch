using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiledMotion : MonoBehaviour
{
    private float timeCounter = 0f;
    [SerializeField] private Transform player;
    void Update()
    {
        timeCounter += Time.deltaTime;

        float x = Mathf.Cos(timeCounter) + player.position.x;
        float y = Mathf.Sin(timeCounter) + player.position.y;
        float z = 0;
        transform.position = new Vector3(x, y, z);
    }
}
