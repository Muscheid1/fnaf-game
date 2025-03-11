using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaptopFlip : MonoBehaviour
{
    private Vector3 rotationAxis = Vector3.right;
    private Vector3 hingePoint;
    private float rotationSpeed = 180f;

    private bool opening = false;
    private bool closing = false;
    private bool open = true;

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
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!opening && !closing)
            {
                if (open)
                {
                    open = false;
                    closing = true;
                }
                else
                {
                    opening = true;
                    multiChannelAudio.PlaySound2();
                }
            }
        }

        if (opening)
        {
            if (GetComponent<Transform>().localRotation.x > 0.0f)
            {
                transform.RotateAround(hingePoint, rotationAxis, rotationSpeed * Time.deltaTime);
            }
            else
            {
                opening = false;
                open = true;
            }
        }
        if (closing)
        {
            if (GetComponent<Transform>().localRotation.x < 0.7f)
            {
                transform.RotateAround(hingePoint, rotationAxis, -rotationSpeed * Time.deltaTime);
            }
            else
            {
                closing = false;
                multiChannelAudio.PlaySound1();
            }
        }
    }
}
