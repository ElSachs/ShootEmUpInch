using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UpdateHighscore : MonoBehaviour
{
    public GameObject highscoreCanvas;
    public GameObject text;
    public void HighscoreAdd()
    {
        highscoreCanvas.SetActive(true);
    }

    public void HighscoreUpdate()
    {
        if(text.GetComponent<TextMeshProUGUI>().text != null)
        {
            if(GameManager.Instance.Score > PlayerPrefs.GetInt("HighScore4"))
            {
                PlayerPrefs.SetString("HighName5", PlayerPrefs.GetString("HighName4"));
                PlayerPrefs.SetInt("HighScore5", PlayerPrefs.GetInt("HighScore4"));
                if (GameManager.Instance.Score > PlayerPrefs.GetInt("HighScore3"))
                {
                    PlayerPrefs.SetString("HighName4", PlayerPrefs.GetString("HighName3"));
                    PlayerPrefs.SetInt("HighScore4", PlayerPrefs.GetInt("HighScore3"));
                    if (GameManager.Instance.Score > PlayerPrefs.GetInt("HighScore2"))
                    {
                        PlayerPrefs.SetString("HighName3", PlayerPrefs.GetString("HighName2"));
                        PlayerPrefs.SetInt("HighScore3", PlayerPrefs.GetInt("HighScore2"));
                        if (GameManager.Instance.Score > PlayerPrefs.GetInt("HighScore1"))
                        {
                            PlayerPrefs.SetString("HighName2", PlayerPrefs.GetString("HighName1"));
                            PlayerPrefs.SetInt("HighScore2", PlayerPrefs.GetInt("HighScore1"));
                            PlayerPrefs.SetString("HighName1", text.GetComponent<TextMeshProUGUI>().text);
                            PlayerPrefs.SetInt("HighScore1", GameManager.Instance.Score);
                        }
                        else
                        {
                            PlayerPrefs.SetString("HighName2", text.GetComponent<TextMeshProUGUI>().text);
                            PlayerPrefs.SetInt("HighScore2", GameManager.Instance.Score);
                        }
                    }
                    else
                    {
                        PlayerPrefs.SetString("HighName3", text.GetComponent<TextMeshProUGUI>().text);
                        PlayerPrefs.SetInt("HighScore3", GameManager.Instance.Score);
                    }
                }
                else
                {
                    PlayerPrefs.SetString("HighName4", text.GetComponent<TextMeshProUGUI>().text);
                    PlayerPrefs.SetInt("HighScore4", GameManager.Instance.Score);
                }
            }
            else
            {
                PlayerPrefs.SetString("HighName5", text.GetComponent<TextMeshProUGUI>().text);
                PlayerPrefs.SetInt("HighScore5", GameManager.Instance.Score);
            }
            
        }
    }
}
