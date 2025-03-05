using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioClip musicOnStart;
    AudioClip switchTo;

    float volume;
    [SerializeField] float maxVolume;
    [SerializeField] float timeToSwitch;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Play(musicOnStart, true);
    }

    public void Play(AudioClip music, bool interrupt = false) // Whenever music needs to be changed, this method can be called.
    {
        if(interrupt)
        {
            volume = maxVolume;
            audioSource.volume = volume;
            audioSource.clip = music;
            audioSource.Play();
        }
        else
        {
            switchTo = music;
            StartCoroutine(SmoothSwitchMusic());
        }
    }

    IEnumerator SmoothSwitchMusic()
    {
        volume = maxVolume;

        while(volume > 0f)
        {
            volume -= Time.deltaTime / (timeToSwitch / maxVolume);
            if (volume < 0f) { volume = 0f; }
            audioSource.volume = volume;
            yield return new WaitForEndOfFrame();
        }

        Play(switchTo, true);
    }
}
