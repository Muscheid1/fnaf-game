using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool open = true;
    private Vector3 switchHingePoint;
    private Transform switchHandle;
    private Vector3 switchRotationAxis;
    private float switchRotationSpeed = 220f;

    private Vector3 doorHingePoint;
    private Transform door;
    private Vector3 doorRotationAxis;
    private float doorRotationSpeed = 220f;

    private Power powerState;
    private BunnyMover bunnyState;

    private bool gameOver = false;

    public string switchHingeName;
    public string switchHandleName;
    public string doorHingeName;
    public string doorName;
    public int killRoom;

    public float switchStart;
    public float switchEnd;
    public int rotationDirection;

    MultiChannelAudio multiChannelAudio;

    // Start is called before the first frame update
    void Start()
    {
        switchHingePoint = GameObject.Find(switchHingeName).GetComponent<Transform>().position;
        switchHandle = GameObject.Find(switchHandleName).GetComponent<Transform>();
        switchRotationAxis = Vector3.forward;

        doorHingePoint = GameObject.Find(doorHingeName).GetComponent<Transform>().position;
        door = GameObject.Find(doorName).GetComponent<Transform>();
        doorRotationAxis = Vector3.up;

        powerState = GameObject.Find("power-display").GetComponent<Power>();
        bunnyState = GameObject.Find("bunny").GetComponent<BunnyMover>();

        multiChannelAudio = GetComponent<MultiChannelAudio>();

    }

    // Update is called once per frame
    void Update()
    {
        if (open || powerState.powerOff)
        {
            if (bunnyState.bunnyIndex == killRoom)
            {
                if (!gameOver)
                {
                    StartCoroutine(bunnyState.GameOverBunny());
                    gameOver = true;
                }
            }

            if (switchHandle.localRotation.x > switchStart)
            {
                switchHandle.RotateAround(switchHingePoint, switchRotationAxis, rotationDirection * switchRotationSpeed * Time.deltaTime);
            }
            if (door.localRotation.z > -0.5f)
            {
                door.RotateAround(doorHingePoint, doorRotationAxis, -doorRotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (switchHandle.localRotation.x < switchEnd)
            {
                switchHandle.RotateAround(switchHingePoint, switchRotationAxis, rotationDirection * -switchRotationSpeed * Time.deltaTime);
            }
            if (door.localRotation.z < 0f)
            {
                door.RotateAround(doorHingePoint, doorRotationAxis, doorRotationSpeed * Time.deltaTime);
            }
        }
    }

    private void OnMouseDown()
    {
        if ((door.localRotation.z <= -0.5f || door.localRotation.z >= 0f) && !gameOver)
        {
            open = !open;
            multiChannelAudio.PlaySound(0);
        }
    }

    private void OnMouseOver()
    {
        
    }
}
