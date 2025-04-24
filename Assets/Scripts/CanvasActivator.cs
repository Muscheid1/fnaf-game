using UnityEngine;

public class CanvasActivator : MonoBehaviour
{
    public GameObject canvas;
    public bool active;
    public TitleScreen titleScreen;

    private void OnMouseDown()
    {
        if (titleScreen.started)
        {
            return;
        }
        canvas.SetActive(active);
    }
}
