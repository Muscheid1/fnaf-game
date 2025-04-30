using UnityEngine;
using TMPro;
using Unity.IO.LowLevel.Unsafe;

public class Usage : MonoBehaviour
{
    private TextMeshPro display;
    public LaptopFlip flipState;
    public Door doorState1;
    public Door doorState2;
    // Start is called before the first frame update
    void Start()
    {
        display = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        display.text = (12 + (flipState.open ? 20  : 0) + (!doorState1.open ? 25 : 0) + (!doorState2.open ? 25 : 0)).ToString();
    }
}
