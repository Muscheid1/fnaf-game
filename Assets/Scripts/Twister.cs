using UnityEngine;

public class Twister : MonoBehaviour
{
    private Quaternion baseRotation;
    private float timer;

    private float twistAmplitude = 5f; // degrees
    private float twistSpeed = 1f;     // radians/sec

    private void Start()
    {
        baseRotation = transform.localRotation;
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Compute oscillating rotation around Y and Z
        float angleOffset = twistAmplitude * Mathf.Sin(twistSpeed * timer);
        Quaternion twistY = Quaternion.Euler(0, angleOffset, 0);
        Quaternion twistZ = Quaternion.Euler(0, 0, angleOffset);

        transform.localRotation = baseRotation * twistY * twistZ;
    }
}