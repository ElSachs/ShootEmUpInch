using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{

    public static SoundController Instance;
    private void Awake()
    {
        Instance = this;
    }

    public GameObject waveManager;
    public AudioClip levelOne;
    public AudioClip levelTwo;
    public AudioClip levelThree;
    public AudioClip bossOne;
    public AudioClip bossTwo;
    public AudioClip bossThree;
    public AudioClip death;
    public AudioClip nextLevel;
    [SerializeField] AudioSource source;
    bool odd;
    bool isTransition;
    private void Start()
    {
        source.clip = levelOne;
        source.Play();
    }
    void Update()
    {
        if(waveManager.GetComponent<WaveManager>().level == 2 && !odd)
        {
            source.loop = true;
            odd = true;
            source.clip = levelTwo;
            source.Play();
            
        }
        if(waveManager.GetComponent<WaveManager>().level == 3 && odd)
        {
            source.loop = true;
            odd = false;
            source.clip = levelThree;
            source.Play();
        }
        if(waveManager.GetComponent<WaveManager>().waveType == 10 && !isTransition)
        {
            isTransition = true;
            PlayBossMusic();
        }
        if (waveManager.GetComponent<WaveManager>().waveType != 10)
        {
            isTransition = false;
        }
    }

    void PlayBossMusic()
    {
        switch (waveManager.GetComponent<WaveManager>().level)
        {
            case 1:
                source.clip = bossOne;
                break;
            case 2:
                source.clip = bossTwo;
                break;
            case 3:
                source.clip = bossThree;
                break;
        }
        source.Play();
    }

    public void PlayDeathSound()
    {
        source.loop = false;
        source.clip = death;
        source.Play();
    }

    public void NextLevel()
    {
        source.loop = false;
        source.clip = nextLevel;
        source.Play();
    }
}
