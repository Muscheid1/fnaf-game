using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleScreen : MonoBehaviour
{
    private TextMeshPro textDisplay;
    private Fade fade;
    // Start is called before the first frame update
    void Start()
    {
        textDisplay = GetComponent<TextMeshPro>();
        if (!PlayerPrefs.HasKey("Night"))
        {
            PlayerPrefs.SetInt("Night", 1);
        }

        textDisplay.text = "Night " + PlayerPrefs.GetInt("Night");
        fade = GameObject.Find("FadeImage").GetComponent<Fade>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        fade.FadeToBlack();
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
