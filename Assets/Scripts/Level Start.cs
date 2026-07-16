using UnityEngine;

public class LevelStart : MonoBehaviour
{
    private Fade fade;
    AudioManager audioManager;

    void Start()
    {
        fade = GameObject.Find("FadeImage").GetComponent<Fade>();
        fade.FadeFromBlack();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.FadeVolume(0f, 1f, "Master");
        audioManager.SetVolume(0f, "Ambience");
        audioManager.SetVolume(6f, "Effects");

    }
}
