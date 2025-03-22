using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public float moveCheck;
    public int moveChance;
    public float totalPowerLoss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        GameDifficulty.bunnyMoveCheck = moveCheck;
        GameDifficulty.bunnyMoveChance = moveChance;
        GameDifficulty.totalPowerLoss = totalPowerLoss;
        SceneManager.LoadScene("Main Scene");
    }
}
