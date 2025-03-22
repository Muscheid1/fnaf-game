using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraSwitch : MonoBehaviour
{
    int index = 0;
    MultiChannelAudio multiChannelAudio;
    LaptopFlip flipState;

    private bool launched = false;
    private bool booted = false;
    private Coroutine blueCoroutine;

    public Material blueMaterial;
    public Material blackMaterial;
    public Material colorMaterial;
    private bool turnedOff = false;

    private Power powerState;
    private BunnyMover bunnyState;
    private int prevBunnyIndex;
    private int prevTeapotIndex;
    private Coroutine colorBarCoroutine;
    private bool colorBar = false;

    private GameObject cameraNumber;
    // Start is called before the first frame update
    void Start()
    {
        multiChannelAudio = GetComponent<MultiChannelAudio>();
        flipState = GameObject.Find("laptop-display").GetComponent<LaptopFlip>();
        powerState = GameObject.Find("power-display").GetComponent<Power>();
        cameraNumber = GameObject.Find("cam-number");
        bunnyState = GameObject.Find("bunny").GetComponent<BunnyMover>();

    }

    // Update is called once per frame
    void Update()
    {
        if (powerState.powerOff) //Black screen when power out
        {
            if (!turnedOff)
            {
                turnedOff = true;
                this.GetComponent<Renderer>().material = blackMaterial;
            }
            return;
        }

        if (flipState.open) //Laptop open
        {
            if (!launched) //Laptop just opened
            {
                launched = true;
                blueCoroutine = StartCoroutine(BlueScreen());
            }
            if (!booted) //Laptop still on blue screen
            {
                return;
            }

            if (prevBunnyIndex != bunnyState.bunnyIndex && (index == prevBunnyIndex || index == bunnyState.bunnyIndex) && !colorBar) //If bunny left or entered camera you're watching
            {
                Debug.Log("bunny");
                colorBar = true;
                colorBarCoroutine = StartCoroutine(ColorBarScreen());
            }

            if (prevTeapotIndex != bunnyState.teapotIndex && (index == prevTeapotIndex || index == bunnyState.teapotIndex) && !colorBar) //If bunny left or entered camera you're watching
            {
                Debug.Log("teapot");
                colorBar = true;
                colorBarCoroutine = StartCoroutine(ColorBarScreen());
            }

            prevBunnyIndex = bunnyState.bunnyIndex;
            prevTeapotIndex = bunnyState.teapotIndex;

            if (Input.GetKeyDown(KeyCode.E))
            {
                index = bunnyState.rooms[index].nextCam;
                cameraNumber.GetComponent<TextMeshPro>().text = "CAM_0" + bunnyState.rooms[index].camNumber;
                if (!colorBar)
                {
                    this.GetComponent<Renderer>().material = bunnyState.rooms[index].camera;
                }
                multiChannelAudio.PlaySound1();
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                index = bunnyState.rooms[index].prevCam;
                cameraNumber.GetComponent<TextMeshPro>().text = "CAM_0" + bunnyState.rooms[index].camNumber;
                if (!colorBar)
                {
                    this.GetComponent<Renderer>().material = bunnyState.rooms[index].camera;
                }
                multiChannelAudio.PlaySound1();
            }
        }
        else //Laptop closed
        {
            launched = false;
            booted = false;
            if (blueCoroutine != null)
            {
                StopCoroutine(blueCoroutine);
            }
            if (colorBarCoroutine != null)
            {
                StopCoroutine(colorBarCoroutine);
                colorBar = false;
            }
        }
    }

    IEnumerator BlueScreen()
    {
        this.GetComponent<Renderer>().material = blueMaterial;
        cameraNumber.SetActive(false);
        yield return new WaitForSeconds(3f);
        if (!powerState.powerOff)
        {
            this.GetComponent<Renderer>().material = bunnyState.rooms[index].camera;
            cameraNumber.SetActive(true);
        }
        booted = true;
    }

    IEnumerator ColorBarScreen()
    {
        this.GetComponent<Renderer>().material = colorMaterial;
        yield return new WaitForSeconds(2f);
        if (!powerState.powerOff)
        {
            this.GetComponent<Renderer>().material = bunnyState.rooms[index].camera;
        }
        colorBar = false;
    }
}
