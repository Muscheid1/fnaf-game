using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Clock : MonoBehaviour
{
    private TextMeshPro textDisplay;
    private float timer;
    private List<string> times;
    public int index;

    private Fade fade;
    public GameObject victoryText;
    public bool victory = false;

    private BunnyMover bunny;

    public AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        textDisplay = GetComponent<TextMeshPro>();
        times = new List<string>();
        times.Add("01:00");
        times.Add("02:00");
        times.Add("03:00");
        times.Add("04:00");
        times.Add("05:00");
        times.Add("06:00");
        index = 0;

        fade = GameObject.Find("VictoryImage").GetComponent<Fade>();
        bunny = GameObject.Find("bunny").GetComponent<BunnyMover>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 70f) //70f
        {
            if (index == 5 && !bunny.gameOver && !victory)
            {
                victory = true;
                StartCoroutine(Victory());
            }
            else
            {
                textDisplay.text = times[index++];
                timer = 0f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Title Screen");
        }
    }

    private IEnumerator Victory()
    {
        int night = PlayerPrefs.GetInt("Night");
        if (night < 5)
        {
            PlayerPrefs.SetInt("Night", night + 1);
        }
        fade.FadeToBlack();
        audioManager.FadeVolume(0f, 1f, "Ambience");
        victoryText.SetActive(true);
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("Title Screen");
    }
}
