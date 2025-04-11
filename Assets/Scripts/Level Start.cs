using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LevelStart : MonoBehaviour
{
    private Fade fade;
    AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        fade = GameObject.Find("FadeImage").GetComponent<Fade>();
        fade.FadeFromBlack();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.FadeVolume(0.112f, 0.7f, "Ambience");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
