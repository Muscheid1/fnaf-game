using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStart : MonoBehaviour
{
    private Fade fade;
    // Start is called before the first frame update
    void Start()
    {
        fade = GameObject.Find("FadeImage").GetComponent<Fade>();
        fade.FadeFromBlack();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
