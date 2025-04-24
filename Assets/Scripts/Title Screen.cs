using UnityEngine;
using TMPro;

public class TitleScreen : MonoBehaviour
{
    private TextMeshPro textDisplay;
    public GameObject nightText;
    public bool started = false;
    public AudioManager audioManager;

    void Start()
    {
        //Night text
        textDisplay = nightText.GetComponent<TextMeshPro>();
        if (!PlayerPrefs.HasKey("Night"))
        {
            PlayerPrefs.SetInt("Night", 1);
        }
        textDisplay.text = "Night " + PlayerPrefs.GetInt("Night");

        //Volume
        audioManager.SetVolume(-4f, "Music");
        audioManager.SetVolume(0f, "Effects");
    }
}
