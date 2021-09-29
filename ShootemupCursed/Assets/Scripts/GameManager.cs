using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singletone
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public enum Bonus
    {
        SpeedBullet, shootBullet
    }

    [System.Serializable]
    public class BonusClass
    {
        public Bonus bonusType;
        public GameObject itemPrefabs;
    }

    public Dictionary<Bonus, GameObject> bonusDictionary = new Dictionary<Bonus, GameObject>();

    public int Score;
    public List<GameObject> Lifes;
    public List<BonusClass> allBonus;
    public TextMeshProUGUI scoreText;
    public int level;

    private void Start()
    {
        foreach (BonusClass clas in allBonus)
        {
            bonusDictionary.Add(clas.bonusType, clas.itemPrefabs);
        }
    }

    public void AddScore(int score)
    {
        Score += score;
        scoreText.text = "Score : " +
                         Environment.NewLine +Score;
    }

    public void UpdateLife()
    {
        Debug.Log(Lifes.Count-1);
       Lifes[Lifes.Count-1].SetActive(false);
       Lifes.Remove(Lifes[Lifes.Count-1]);
       
    }

    public void InstantKill()
    {
        for (int i = 0; i < Lifes.Count; i++)
        {
           UpdateLife(); 
        }
    }
}
