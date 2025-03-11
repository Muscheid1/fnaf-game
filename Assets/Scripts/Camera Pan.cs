using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{
    Vector3 mousePos;
    Quaternion cameraPan;

    float rotateLimit = 0.3f;
    float deadZone = 0.05f;
    int screenWidth;
    float accelerationFactor = 0.008f;

    // Start is called before the first frame update
    void Start()
    {
        screenWidth = Screen.width;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        cameraPan = Camera.main.transform.rotation;

        if (mousePos.x / screenWidth >= 0.5f + deadZone && cameraPan.y < rotateLimit) 
        {
            cameraPan.y += accelerationFactor * (mousePos.x / screenWidth - 0.5f - deadZone);
            Camera.main.transform.rotation = cameraPan;
        }
        else if (mousePos.x / screenWidth <= 0.5f - deadZone && cameraPan.y > -rotateLimit)
        {
            cameraPan.y -= accelerationFactor * (0.5f - deadZone - mousePos.x / screenWidth);
            Camera.main.transform.rotation = cameraPan;
        }
    }
}
