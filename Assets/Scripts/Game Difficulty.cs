using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameDifficulty
{
    public static List<float> bunnyMoveCheck;
    public static List<float> teapotMoveCheck;
    public static List<int> bunnyMoveChance;
    public static List<int> teapotMoveChance;

    static GameDifficulty()
    {
        bunnyMoveCheck = new List<float>();
        bunnyMoveChance = new List<int>();
        teapotMoveCheck = new List<float>();
        teapotMoveChance = new List<int>();
        //Night 1
        //12AM
        bunnyMoveCheck.Add(18f); // 18f
        bunnyMoveChance.Add(20); //20
        teapotMoveCheck.Add(20f); //20f
        teapotMoveChance.Add(0); //0
        //2AM
        bunnyMoveCheck.Add(18f);
        bunnyMoveChance.Add(40);
        teapotMoveCheck.Add(20f);
        teapotMoveChance.Add(0);

        //4AM
        bunnyMoveCheck.Add(18f);
        bunnyMoveChance.Add(50);
        teapotMoveCheck.Add(20f);
        teapotMoveChance.Add(0);


        //Night 2
        //12AM
        bunnyMoveCheck.Add(16f);
        bunnyMoveChance.Add(20);
        teapotMoveCheck.Add(15f);
        teapotMoveChance.Add(10);

        //2AM
        bunnyMoveCheck.Add(16f);
        bunnyMoveChance.Add(40);
        teapotMoveCheck.Add(15f);
        teapotMoveChance.Add(20);

        //4AM
        bunnyMoveCheck.Add(16f);
        bunnyMoveChance.Add(50);
        teapotMoveCheck.Add(15f);
        teapotMoveChance.Add(35);


        //Night 3
        //12AM
        bunnyMoveCheck.Add(14f);
        bunnyMoveChance.Add(30);
        teapotMoveCheck.Add(13f);
        teapotMoveChance.Add(30);

        //2AM
        bunnyMoveCheck.Add(14f);
        bunnyMoveChance.Add(40);
        teapotMoveCheck.Add(13f);
        teapotMoveChance.Add(40);

        //4AM
        bunnyMoveCheck.Add(14f);
        bunnyMoveChance.Add(50);
        teapotMoveCheck.Add(13f);
        teapotMoveChance.Add(50);


        //Night 4
        //12AM
        bunnyMoveCheck.Add(12f);
        bunnyMoveChance.Add(40);
        teapotMoveCheck.Add(11f);
        teapotMoveChance.Add(40);

        //2AM
        bunnyMoveCheck.Add(12f);
        bunnyMoveChance.Add(50);
        teapotMoveCheck.Add(11f);
        teapotMoveChance.Add(50);

        //4AM
        bunnyMoveCheck.Add(12f);
        bunnyMoveChance.Add(60);
        teapotMoveCheck.Add(11f);
        teapotMoveChance.Add(60);


        //Night 5
        //12AM
        bunnyMoveCheck.Add(10f);
        bunnyMoveChance.Add(50);
        teapotMoveCheck.Add(9f);
        teapotMoveChance.Add(50);

        //2AM
        bunnyMoveCheck.Add(10f);
        bunnyMoveChance.Add(60);
        teapotMoveCheck.Add(9f);
        teapotMoveChance.Add(60);

        //4AM
        bunnyMoveCheck.Add(10f);
        bunnyMoveChance.Add(70);
        teapotMoveCheck.Add(9f);
        teapotMoveChance.Add(70);


    }
}
