using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasActivator : MonoBehaviour
{
    public GameObject canvas;
    public bool active;

    private void OnMouseDown()
    {
        canvas.SetActive(active);
    }
}
