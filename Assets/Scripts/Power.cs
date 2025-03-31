using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Power : MonoBehaviour
{
    private float power;
    private LaptopFlip flipState;
    private Door doorState1;
    private Door doorState2;
    public bool powerOff = false;

    private float basicLoss = 0.15f;
    private float laptopLoss = 0.15f;
    private float doorLoss = 0.2f;
    private float totalLoss = 0.8f;

    private TextMeshPro textDisplay;

    // Start is called before the first frame update
    void Start()
    {
        power = 100f;
        flipState = GameObject.Find("laptop-display").GetComponent<LaptopFlip>();
        doorState1 = GameObject.Find("switch-1").GetComponent<Door>();
        doorState2 = GameObject.Find("switch-2").GetComponent<Door>();
        textDisplay = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        power -= (basicLoss + Bti(flipState.open) * laptopLoss + Bti(!(doorState1.open)) * doorLoss + Bti(!(doorState2.open)) * doorLoss) * Time.deltaTime * totalLoss;
        if (power >= 0f)
        {
            textDisplay.text = ((int)Math.Floor(power)).ToString();
        }
        else
        {
            powerOff = true;
        }

    }

    private int Bti(bool param)
    {
        return param ? 1 : 0;
    }
}
