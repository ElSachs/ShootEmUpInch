using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetExplosion : MonoBehaviour
{
    void Update()
    {
        if (!GetComponent<ParticleSystem>().isPlaying) gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        GetComponent<ParticleSystem>().Play();
    }
}
