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
    int index;

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
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 60f)
        {
            if (index == 5)
            {
                SceneManager.LoadScene("Title Screen");
                return;
            }
            textDisplay.text = times[index++];
            timer = 0f;
        }
    }
}
