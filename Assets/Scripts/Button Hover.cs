using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHover : MonoBehaviour
{
    public GameObject square;
    public Vector3 offset;
    GameObject currSquare;
    private void OnMouseEnter()
    {
        currSquare = Instantiate(square);
        currSquare.transform.position = transform.position + offset;
    }

    private void OnMouseExit()
    {
        if (currSquare != null)
        {
            Destroy(currSquare);
        }
    }

    private void OnMouseDown()
    {
        Destroy(currSquare);
    }
}
