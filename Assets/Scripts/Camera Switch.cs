using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    Material[] materials;
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        materials = Resources.LoadAll<Material>("Materials");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            index = (index + 1) % materials.Length;
            this.GetComponent<Renderer>().material = materials[index];
        }
    }
}
