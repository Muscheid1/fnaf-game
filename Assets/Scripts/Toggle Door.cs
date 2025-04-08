using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglerDoor : MonoBehaviour
{
    public bool open = true;
    private Vector3 switchHingePoint;
    private Transform switchHandle;
    private Vector3 switchRotationAxis;
    private float switchRotationSpeed = 220f;

    private Vector3 doorHingePoint1;
    private Transform door1;
    private Vector3 doorHingePoint2;
    private Transform door2;

    private Vector3 doorRotationAxis;
    private float doorRotationSpeed = 220f;

    private Power powerState;
    private BunnyMover bunnyState;

    private bool gameOver = false;

    private float switchStart = -0.5373f;
    private float switchEnd = 0f;
    private int rotationDirection = -1;

    MultiChannelAudio multiChannelAudio;

    // Start is called before the first frame update
    void Start()
    {
        switchHingePoint = GameObject.Find("switch-hinge-3").GetComponent<Transform>().position;
        switchHandle = GameObject.Find("switch-handle-3").GetComponent<Transform>();
        switchRotationAxis = Vector3.right;

        doorHingePoint1 = GameObject.Find("door-hinge-3").GetComponent<Transform>().position;
        door1 = GameObject.Find("door-3").GetComponent<Transform>();
        doorHingePoint2 = GameObject.Find("door-hinge-4").GetComponent<Transform>().position;
        door2 = GameObject.Find("door-4").GetComponent<Transform>();

        doorRotationAxis = Vector3.up;

        powerState = GameObject.Find("power-display").GetComponent<Power>();
        bunnyState = GameObject.Find("bunny").GetComponent<BunnyMover>();

        multiChannelAudio = GetComponent<MultiChannelAudio>();
    }

    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            if (bunnyState.teapotIndex == 12)
            {
                if (!gameOver)
                {
                    StartCoroutine(bunnyState.GameOverTeapot());
                    gameOver = true;
                }
            }

            if (switchHandle.localRotation.x > switchStart)
            {
                switchHandle.RotateAround(switchHingePoint, switchRotationAxis, rotationDirection * switchRotationSpeed * Time.deltaTime);
            }
            if (door1.localRotation.z > -0.5f)
            {
                door1.RotateAround(doorHingePoint1, doorRotationAxis, -doorRotationSpeed * Time.deltaTime);
            }
            if (door2.localRotation.z < 0f)
            {
                door2.RotateAround(doorHingePoint2, doorRotationAxis, doorRotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (bunnyState.teapotIndex == 11)
            {
                if (!gameOver)
                {
                    StartCoroutine(bunnyState.GameOverTeapot());
                    gameOver = true;
                }
            }
            if (switchHandle.localRotation.x < switchEnd)
            {
                switchHandle.RotateAround(switchHingePoint, switchRotationAxis, rotationDirection * -switchRotationSpeed * Time.deltaTime);
            }
            if (door1.localRotation.z < 0f)
            {
                door1.RotateAround(doorHingePoint1, doorRotationAxis, doorRotationSpeed * Time.deltaTime);
            }
            if (door2.localRotation.z > -0.5f)
            {
                door2.RotateAround(doorHingePoint2, doorRotationAxis, -doorRotationSpeed * Time.deltaTime);
            }
        }
    }

    private void OnMouseDown()
    {
        if (door1.localRotation.z <= -0.5f || door1.localRotation.z >= 0f)
        {
            open = !open;
            multiChannelAudio.PlaySound(0);
        }
    }

    private void OnMouseOver()
    {

    }
}
