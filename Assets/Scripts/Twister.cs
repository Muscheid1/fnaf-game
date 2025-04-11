using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twister : MonoBehaviour
{
    Quaternion left;
    Quaternion right;
    float timer = 0f;

    float startZ;
    private void Start()
    {
        startZ = transform.localRotation.z;
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        Vector3 angles = transform.localEulerAngles;
        float x = angles.x;
        float y = angles.y + 0.02f * Mathf.Sin(2 * timer);
        float z = angles.z + 0.02f * Mathf.Sin(2 * timer);

        Quaternion newRotation = Quaternion.Euler(x, y, z);
        transform.localRotation = newRotation;
    }
}
