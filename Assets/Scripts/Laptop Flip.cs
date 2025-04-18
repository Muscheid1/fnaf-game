using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaptopFlip : MonoBehaviour
{
    private Vector3 rotationAxis = Vector3.right;
    private Vector3 hingePoint;
    private float rotationSpeed = 220f;

    public bool open = false;

    public bool clicked = true;

    private bool closedSound = true;

    MultiChannelAudio multiChannelAudio;
    // Start is called before the first frame update
    void Start()
    {
        hingePoint = GameObject.Find("hinge").GetComponent<Transform>().position;
        multiChannelAudio = GetComponent<MultiChannelAudio>();
    }

    // Update is called once per frame
    void Update()
    {
        if (open) //Opening
        {
            clicked = false;
            closedSound = false;
            if (GetComponent<Transform>().localRotation.x > -0.1f)
            {
                transform.RotateAround(hingePoint, rotationAxis, rotationSpeed * Time.deltaTime);
            }
            else //Fully open
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    open = false;
                }
            }
        }
        else //Closing
        {
            if (GetComponent<Transform>().localRotation.x < 0.6980147f) //0.7040147f
            {
                transform.RotateAround(hingePoint, rotationAxis, -rotationSpeed * Time.deltaTime);
            }
            else //Fully closed
            {
                clicked = true;
                if (!closedSound)
                {
                    multiChannelAudio.PlaySound(0);
                    closedSound = true;
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    open = true;
                    multiChannelAudio.PlaySound(1);
                }
            }
        }
    }
}
