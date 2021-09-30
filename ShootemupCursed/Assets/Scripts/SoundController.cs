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
    public AudioClip boss;
    public AudioClip transition;
    public AudioClip death;
    public AudioClip nextLevel;
    bool isTransition;
    [SerializeField] AudioSource source;
    private void Start()
    {
        source.clip = levelOne;
        source.Play();
    }
    void Update()
    {
        if(waveManager.GetComponent<WaveManager>().waveType == 10 && !isTransition)
        {
            isTransition = true;
            source.clip = transition;
            source.Play();
            source.loop = false;
            StartCoroutine(WaitForAudio(transition));
        }
    }

    private IEnumerator WaitForAudio(AudioClip clip)
    {
        while (source.isPlaying)
        {
            yield return null;
        }
        PlayBossMusic();
    }

    void PlayBossMusic()
    {
        source.loop = true;
        source.clip = boss;
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
