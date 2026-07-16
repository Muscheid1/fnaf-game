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
        public bool loop;
    }

    public List<SoundElement> sounds = new List<SoundElement>();
    private int soundIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public GameObject PlayRandomSound()
    {
        soundIndex = Random.Range(0, sounds.Count);
        return SoundLogic();
    }

    public GameObject PlaySound(int i)
    {
        soundIndex = i;
        return SoundLogic();
    }

    private GameObject SoundLogic()
    {
        GameObject tempAudioObj = new GameObject("TempAudio");
        tempAudioObj.transform.position = transform.position;

        // Add AudioSource component
        AudioSource tempSource = tempAudioObj.AddComponent<AudioSource>();
        tempSource.clip = sounds[soundIndex].clip;
        tempSource.outputAudioMixerGroup = sounds[soundIndex].mixerGroup;
        tempSource.spatialBlend = sounds[soundIndex].spatialBlend;
        tempSource.volume = sounds[soundIndex].volume;
        tempSource.loop = sounds[soundIndex].loop;
        tempSource.Play();

        // Destroy the GameObject after the clip finishes playing (if not set to loop)
        if (!sounds[soundIndex].loop)
        {
            Destroy(tempAudioObj, sounds[soundIndex].clip.length);
        }
        return tempAudioObj;
    }
}
