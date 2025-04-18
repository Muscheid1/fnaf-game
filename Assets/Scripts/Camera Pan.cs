using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraPan : MonoBehaviour
{
    Vector3 mousePos;

    float deadZone = 0.08f;
    int screenWidth;

    private Vector3 rotationSpeed = new Vector3(0f, 300f, 0f);
    private BunnyMover bunnyState;

    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        bunnyState = GameObject.Find("bunny").GetComponent<BunnyMover>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver && bunnyState.bunnyOverLeft)
        {
            StartCoroutine(BunnyOver(320f));
            gameOver = true;
        }
        else if (!gameOver && bunnyState.bunnyOverRight)
        {
            StartCoroutine(BunnyOver(60f));
            gameOver = true;
        }
        else if (!gameOver && bunnyState.teapotOverLeft)
        {
            StartCoroutine(BunnyOver(150f));
            gameOver = true;
        }
        else if (!gameOver && bunnyState.teapotOverRight)
        {
            StartCoroutine(BunnyOver(210f));
            gameOver = true;
        }
        if (gameOver) { return; }

        screenWidth = Screen.width;
        mousePos = Input.mousePosition;

        if (mousePos.x / screenWidth >= 0.5f + deadZone) //&& cameraPan.y < rotateLimit
        {
            Camera.main.transform.Rotate(Time.deltaTime * rotationSpeed * (mousePos.x / screenWidth - 0.5f - deadZone));
        }
        else if (mousePos.x / screenWidth <= 0.5f - deadZone)
        {
            Camera.main.transform.Rotate(-Time.deltaTime * rotationSpeed * (0.5f - deadZone - mousePos.x / screenWidth));
        }
    }


    private IEnumerator BunnyOver(float targetAngle)
    {
        float turnTimer = 0.4f;

        Quaternion startRotation = Camera.main.transform.rotation;
        Vector3 startEuler = startRotation.eulerAngles;
        Vector3 endEuler = new Vector3(startEuler.x, targetAngle, startEuler.z);
        Quaternion endRotation = Quaternion.Euler(endEuler);

        float elapsed = 0f;

        while (elapsed < turnTimer)
        {
            float t = elapsed / turnTimer;
            Camera.main.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        yield return null;
    }
}
