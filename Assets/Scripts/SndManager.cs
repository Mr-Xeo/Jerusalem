using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SndManager : MonoBehaviour
{
    public AudioSource footstep;
    public float footstepFadingTime;
    private bool isFootstepStarted;
    private float initialFootstepVolume;

    void Start()
    {
        initialFootstepVolume = footstep.volume;
    }

    void Update()
    {
        if(Player.isWalking)
        {
            StartCoroutine(FadeIn(footstep, footstepFadingTime));
        }

        else
        {
            isFootstepStarted = false;
            StartCoroutine(FadeOut(footstep, footstepFadingTime));
        }
    }

    IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }
        audioSource.Stop();
    }

    IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        if (!isFootstepStarted)
        {
            isFootstepStarted = true;
            audioSource.Play();
            audioSource.time = Random.Range(0, audioSource.clip.length);
            audioSource.volume = 0f;
            while (audioSource.volume < 1)
            {
                audioSource.volume += Time.deltaTime / FadeTime;
                yield return null;
            }
        }
    }
}
