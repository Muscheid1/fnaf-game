using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHover : MonoBehaviour
{
    MultiChannelAudio multiChannelAudio;
    public GameObject square;
    public Vector3 offset;
    GameObject currSquare;
    private void Start()
    {
        multiChannelAudio = GameObject.Find("AudioManager").GetComponent<MultiChannelAudio>();
    }

    private void OnMouseEnter()
    {
        currSquare = Instantiate(square);
        currSquare.transform.position = transform.position + offset;
        multiChannelAudio.PlaySound(0);
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
