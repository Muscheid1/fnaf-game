using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{
    Vector3 mousePos;

    float deadZone = 0.05f;
    int screenWidth;

    private Vector3 rotationSpeed = new Vector3(0f, 250f, 0f);

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
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
}
