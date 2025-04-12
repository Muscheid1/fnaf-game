using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    private Fade fade;
    public AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        fade = GameObject.Find("FadeImage").GetComponent<Fade>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        PlayerPrefs.SetInt("Night", 1);
        fade.FadeToBlack();
        audioManager.FadeVolume(0f, 1f, "Music");
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
