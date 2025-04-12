using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAudioBegin : MonoBehaviour
{
    public AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        audioManager.SetVolume(0.631f, "Music");
    }

}
