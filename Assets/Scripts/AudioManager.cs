using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mixer;


    public void SetVolume(float volume, string channel)
    {
        // Convert from [0,1] linear to decibels [-80, 0]
        //float dB = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20;
        mixer.SetFloat(channel, volume);
    }

    public void FadeVolume(float volume, float timer, string channel) {

        StartCoroutine(VolumeFader(volume, timer, channel));
    }

    private IEnumerator VolumeFader(float finishDB, float timer, string channel)
    {
        float startDB;
        mixer.GetFloat(channel, out startDB);
        //float finishdB = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20;

        float fadeTimer = 0f;

        while (fadeTimer < timer)
        {
            mixer.SetFloat(channel, startDB + (finishDB - startDB) * fadeTimer / timer);
            fadeTimer += Time.deltaTime;
            yield return null;
        }
    }
}
