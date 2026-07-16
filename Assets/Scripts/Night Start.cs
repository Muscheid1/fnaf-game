using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NightStart : MonoBehaviour
{
    private Fade fade;
    public AudioManager audioManager;
    public bool newGame;
    public TitleScreen titleScreen;

    void Start()
    {
        fade = GameObject.Find("FadeImage").GetComponent<Fade>();
    }

    void OnMouseDown()
    {
        if (titleScreen.started)
        {
            return;
        }
        titleScreen.started = true;
        if (newGame)
        {
            PlayerPrefs.SetInt("Night", 1);
            PlayerPrefs.SetInt("Star", 0);
        }
        fade.FadeToBlack();
        audioManager.FadeVolume(-80f, 1f, "Master");
        StartCoroutine(SceneLoader());
    }

    private IEnumerator SceneLoader()
    {
        float timer = 0f;
        while (timer < 1f)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadScene("Main Scene");
    }
}
