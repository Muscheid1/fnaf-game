using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOV : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 posititon = this.GetComponent<Transform>().position;
        posititon.x += 0.005f;
        this.GetComponent<Transform>().position = posititon;
    }
}
