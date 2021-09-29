using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    [SerializeField] private PoolManager.Generate typeOfBonus;
    private void Update()
    {
        transform.Translate(0f, -1f*Time.deltaTime, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("getting");
        
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("get");
            other.gameObject.GetComponent<PlayerController>().AddBonus(typeOfBonus);
        }
        gameObject.SetActive(false);
    }
    
}
