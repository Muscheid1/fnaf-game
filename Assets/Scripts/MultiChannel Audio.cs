using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MultiChannelAudio : MonoBehaviour
{
    [System.Serializable]
    public class SoundElement
    {
        public AudioClip clip;
        public float volume;
        public AudioMixerGroup mixerGroup;
        public float spatialBlend;
    }

    public List<SoundElement> sounds = new List<SoundElement>();
    private int soundIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public void PlayRandomSound()
    {
        soundIndex = Random.Range(0, sounds.Count);
        SoundLogic();
    }

    public void PlaySound(int i)
    {
        soundIndex = i;
        SoundLogic();
    }

    private void SoundLogic()
    {
        GameObject tempAudioObj = new GameObject("TempAudio");
        tempAudioObj.transform.position = transform.position;

        // Add AudioSource component
        AudioSource tempSource = tempAudioObj.AddComponent<AudioSource>();
        tempSource.clip = sounds[soundIndex].clip;
        tempSource.outputAudioMixerGroup = sounds[soundIndex].mixerGroup;
        tempSource.spatialBlend = sounds[soundIndex].spatialBlend;
        tempSource.volume = sounds[soundIndex].volume;
        //tempSource.volume = volume;
        tempSource.Play();

        // Destroy the GameObject after the clip finishes playing
        Destroy(tempAudioObj, sounds[soundIndex].clip.length);
    }
}
