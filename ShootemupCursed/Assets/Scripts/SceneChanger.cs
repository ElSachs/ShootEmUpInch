using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene (int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    public void DestroyManager()
    {
        Destroy(GameManager.Instance.gameObject);
    }
}
