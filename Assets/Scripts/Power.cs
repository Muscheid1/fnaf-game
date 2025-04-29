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

    private float basicLoss = 0.12f;
    private float laptopLoss = 0.2f;
    private float doorLoss = 0.25f;
    private float totalLoss = 0.9f; //0.9f

    private TextMeshPro textDisplay;

    GameObject lights;
    GameObject deskLamp;
    public GameObject backupLights;
    GameObject powerTextGroup;
    GameObject litComponents;
    GameObject lampWhite;
    public Material lampOff;

    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        power = 100f;
        flipState = GameObject.Find("laptop-display").GetComponent<LaptopFlip>();
        doorState1 = GameObject.Find("switch-1").GetComponent<Door>();
        doorState2 = GameObject.Find("switch-2").GetComponent<Door>();
        textDisplay = GetComponent<TextMeshPro>();
        lights = GameObject.Find("Lights");
        deskLamp = GameObject.Find("Desk Lamp");
        powerTextGroup = GameObject.Find("powertextgroup");
        litComponents = GameObject.Find("Lit Components");
        lampWhite = GameObject.Find("lamp-white");

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        power -= (basicLoss + Bti(flipState.open) * laptopLoss + Bti(!(doorState1.open)) * doorLoss + Bti(!(doorState2.open)) * doorLoss) * Time.deltaTime * totalLoss;
        if (power >= 0f)
        {
            textDisplay.text = ((int)Math.Floor(power)).ToString();
        }
        else if (powerOff == false)
        {
            powerOff = true;
            lights.SetActive(false);
            deskLamp.SetActive(false);
            backupLights.SetActive(true);
            powerTextGroup.SetActive(false);
            litComponents.SetActive(false);
            lampWhite.GetComponent<Renderer>().material = lampOff;
            audioManager.FadeVolume(-80f, 5f, "Ambience");
        }

    }

    private int Bti(bool param)
    {
        return param ? 1 : 0;
    }
}
