using UnityEngine;
using TMPro;

public class TitleScreen : MonoBehaviour
{
    private TextMeshPro textDisplay;
    public GameObject nightText;
    public bool started = false;
    public AudioManager audioManager;
    public GameObject star;

    void Start()
    {
        //Night text
        textDisplay = nightText.GetComponent<TextMeshPro>();
        if (!PlayerPrefs.HasKey("Night"))
        {
            PlayerPrefs.SetInt("Night", 1);
        }
        textDisplay.text = "Night " + PlayerPrefs.GetInt("Night");

        //Completion star
        if (!PlayerPrefs.HasKey("Star"))
        {
            PlayerPrefs.SetInt("Star", 0);
        }
        if (PlayerPrefs.GetInt("Star") == 1)
        {
            star.SetActive(true);
        }

        //Volume
        audioManager.SetVolume(2f, "Music");
        audioManager.SetVolume(0f, "Effects");
    }
}
