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
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    

    public int Score;
    public PlayerController player;
    public List<GameObject> Lifes;
    public TextMeshProUGUI scoreText;

    public Material blueMaterial;
    public Material redMaterial;
    public GameObject[] border;

    public GameObject healthBar;

    public int level;

    public AudioSource lifeMinus;
    public GameObject player;

    public void AddScore(int score)
    {
        Score += score;
        scoreText.text = "Score : " +
                         Environment.NewLine +Score;
    }

    public void UpdateLife()
    {
        if (Lifes[Lifes.Count-1] != null)
        {
            
            Debug.Log(Lifes.Count-1);
           Lifes[Lifes.Count-1].SetActive(false);
           Lifes.Remove(Lifes[Lifes.Count-1]);
        }
        lifeMinus.Play();
        StartCoroutine(BorderDamage());
        GameObject.Find("Main Camera").GetComponent<ShakeCam>().TriggerShake();
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

    public void UpdateBorder(bool Blue)
    {
        if (Blue)
        {
            foreach (GameObject gam in border)
            {
                gam.GetComponent<MeshRenderer>().material = blueMaterial;
            }
        }
        else
        {
            foreach (GameObject gam in border)
            {
                gam.GetComponent<MeshRenderer>().material = redMaterial;
            }
        }
    }

    public void EndAnimation()
    {
        player.canMove = false;
        player.isTransiting = true;
    }
    
    IEnumerator BorderDamage()
    {
        if(!player.GetComponent<PlayerController>().Isblue) UpdateBorder(true);
        yield return new WaitForSeconds(0.1f);
        UpdateBorder(false);
        yield return new WaitForSeconds(0.1f);
        UpdateBorder(true);
        yield return new WaitForSeconds(0.1f);
        UpdateBorder(false);
        yield return new WaitForSeconds(0.1f);
        UpdateBorder(true);
        yield return new WaitForSeconds(0.1f);
        UpdateBorder(false);
        yield return new WaitForSeconds(0.1f);
        if (player.GetComponent<PlayerController>().Isblue) UpdateBorder(true);
    
}
