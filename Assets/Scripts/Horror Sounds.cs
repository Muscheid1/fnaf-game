using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorrorSounds : MonoBehaviour
{
    MultiChannelAudio multiChannelAudio;
    float timer;
    float cap;
    bool playing;
    GameObject soundObject;

    private void Start()
    {
        multiChannelAudio = GetComponent<MultiChannelAudio>();
        timer = 0f;
        playing = false;
        cap = UnityEngine.Random.Range(40f, 70f);
    }

    private void Update()
    {
        if (soundObject != null)
        {
            return;
        }
        else if (playing == true)
        {
            playing = false;
            cap = UnityEngine.Random.Range(40f, 70f);
            timer = 0f;
        }

        timer += Time.deltaTime;
        if (timer > cap)
        {
            soundObject = multiChannelAudio.PlayRandomSound();
            playing = true;
        }
    }
}
