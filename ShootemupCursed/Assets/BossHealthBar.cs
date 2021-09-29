using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public static BossHealthBar Instance;

    private void Awake()
    {
        Instance = this;
    }


    [SerializeField] private Slider slider;
    [SerializeField] public GameObject boss;
    private int health = 1;

    private void Start()
    {
        
        Debug.Log("c'est moi weshhhh");
        gameObject.SetActive(true);
        health = boss.GetComponent<BossBehaviours>().life;
        SetMaxHealth(health);
    }

    void Update()
    {
        health = boss.GetComponent<BossBehaviours>().life;
        Debug.Log(health);
        SetHealth(health);
    }

    private void SetMaxHealth(int life)
    {
        slider.maxValue = life;
        slider.value = life;
    }
    private void SetHealth(int life)
    {
        slider.value = life;
    }
}
