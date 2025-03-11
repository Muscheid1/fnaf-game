using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiChannelAudio : MonoBehaviour
{
    public AudioClip a1;
    public AudioClip a2;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void PlaySound1()
    {
        audioSource.clip = a1;
        audioSource.Play();
    }

    public void PlaySound2()
    {
        audioSource.clip = a2;
        audioSource.Play();
    }


}
