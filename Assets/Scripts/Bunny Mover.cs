using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BunnyMover : MonoBehaviour
{
    Material[] cameras;

    float bunnyTimer = 0f;
    public int bunnyIndex = 0;
    float bunnyMoveCheck;
    int bunnyMoveChance;

    float teapotTimer = 0f;
    public int teapotIndex = 0;
    float teapotMoveCheck;
    int teapotMoveChance;

    private GameObject teapot;
    private Clock clock;

    public List<Room> rooms;

    public bool bunnyOverLeft = false;
    public bool bunnyOverRight = false;
    public bool teapotOverLeft = false;
    public bool teapotOverRight = false;

    public bool gameOver = false;

    MultiChannelAudio bunnyAudio;
    MultiChannelAudio teapotAudio;
    private Power powerState;

    void Start()
    {
        powerState = GameObject.Find("power-display").GetComponent<Power>();

        bunnyAudio = GetComponent<MultiChannelAudio>();
        teapotAudio = GameObject.Find("teapot").GetComponent<MultiChannelAudio>();

        cameras = Resources.LoadAll<Material>("Materials");

        rooms = new List<Room>();

        Room room;

        //Index 0
        room = new Room();

        room.bunnyPosition = new Vector3(-20.2f, 8.78f, 83.68f);
        room.bunnyAdjacentRooms.Add(1);
        room.bunnyAdjacentRooms.Add(2);

        room.teapotPosition = new Vector3(-4.27f, 0.59f, 79f);
        room.teapotAdjacentRooms.Add(1);
        room.teapotAdjacentRooms.Add(2);

        room.prevCam = 10;
        room.nextCam = 1;
        room.camera = cameras[0];
        room.camNumber = "1";
        rooms.Add(room);

        //Index 1
        room = new Room();

        room.bunnyPosition = new Vector3(-40.3f, 3.9f, 101.6f);
        room.bunnyAdjacentRooms.Add(2);
        room.bunnyAdjacentRooms.Add(3);

        room.teapotPosition = new Vector3(-47.24f, -1.29f, 88.57f);
        room.teapotAdjacentRooms.Add(2);
        room.teapotAdjacentRooms.Add(7);

        room.prevCam = 0;
        room.nextCam = 2;
        room.camera = cameras[1];
        room.camNumber = "2";
        rooms.Add(room);

        //Index 2
        room = new Room();

        room.bunnyPosition = new Vector3(76.4f, -11.28f, 61.5f);
        room.bunnyAdjacentRooms.Add(1);
        room.bunnyAdjacentRooms.Add(4);

        room.teapotPosition = new Vector3(77.42f, -8.8f, 66.7f);
        room.teapotAdjacentRooms.Add(1);
        room.teapotAdjacentRooms.Add(8);

        room.prevCam = 1;
        room.nextCam = 3;
        room.camera = cameras[2];
        room.camNumber = "3";
        rooms.Add(room);

        //Index 3
        room = new Room();

        room.bunnyPosition = new Vector3(-55.47f, 31.7f, 33f);
        room.bunnyAdjacentRooms.Add(4);
        room.bunnyAdjacentRooms.Add(5);

        room.prevCam = 2;
        room.nextCam = 4;
        room.camera = cameras[3];
        room.camNumber = "4";
        rooms.Add(room);

        //Index 4
        room = new Room();
        room.bunnyPosition = new Vector3(66.64f, 23.88f, 20.53f);
        room.bunnyAdjacentRooms.Add(3);
        room.bunnyAdjacentRooms.Add(6);
        room.prevCam = 3;
        room.nextCam = 7;
        room.camera = cameras[4];
        room.camNumber = "5";
        rooms.Add(room);

        //Index 5
        room = new Room();
        room.bunnyPosition = new Vector3(-9.9f, -0.38f, 11.4f);
        room.bunnyAdjacentRooms.Add(0);
        rooms.Add(room);

        //Index 6
        room = new Room();
        room.bunnyPosition = new Vector3(12.9f, 0.3f, 14.32f);
        room.bunnyAdjacentRooms.Add(0);
        rooms.Add(room);

        //Index 7
        room = new Room();

        room.teapotPosition = new Vector3(-39.5f, -1.04f, 10.11f);
        room.teapotAdjacentRooms.Add(9);

        room.prevCam = 4;
        room.nextCam = 8;
        room.camera = cameras[5];
        room.camNumber = "6";
        rooms.Add(room);

        //Index 8
        room = new Room();

        room.teapotPosition = new Vector3(74.8f, -1.04f, -12.4f);
        room.teapotAdjacentRooms.Add(10);

        room.prevCam = 7;
        room.nextCam = 9;
        room.camera = cameras[6];
        room.camNumber = "7";
        rooms.Add(room);

        //Index 9
        room = new Room();

        room.teapotPosition = new Vector3(-33.2f, -23.4f, -25.5f);
        room.teapotAdjacentRooms.Add(11);

        room.prevCam = 8;
        room.nextCam = 10;
        room.camera = cameras[7];
        room.camNumber = "8";
        rooms.Add(room);

        //Index 10
        room = new Room();

        room.teapotPosition = new Vector3(24f, -23.4f, -25.5f);
        room.teapotAdjacentRooms.Add(12);

        room.prevCam = 9;
        room.nextCam = 0;
        room.camera = cameras[8];
        room.camNumber = "9";
        rooms.Add(room);

        //Index 11
        room = new Room();
        room.teapotPosition = new Vector3(-20.1f, -23.4f, 13.4f);
        room.teapotAdjacentRooms.Add(0);
        rooms.Add(room);

        //Index 12
        room = new Room();
        room.teapotPosition = new Vector3(31f, -23.4f, 13.4f);
        room.teapotAdjacentRooms.Add(0);
        rooms.Add(room);
    }

    private void Awake()
    {
        teapot = GameObject.Find("teapot");
        transform.position = new Vector3(-20.2f, 8.78f, 83.68f);
        teapot.transform.position = new Vector3(-4.27f, 0.59f, 79f);

        clock = GameObject.Find("Time").GetComponent<Clock>();
    }

    void Update()
    {
        if (!powerState.powerOff)
        {
            bunnyMoveCheck = GameDifficulty.bunnyMoveCheck[(PlayerPrefs.GetInt("Night") - 1) * 3 + clock.index / 2];
            bunnyMoveChance = GameDifficulty.bunnyMoveChance[(PlayerPrefs.GetInt("Night") - 1) * 3 + clock.index / 2];
            teapotMoveCheck = GameDifficulty.teapotMoveCheck[(PlayerPrefs.GetInt("Night") - 1) * 3 + clock.index / 2];
            teapotMoveChance = GameDifficulty.teapotMoveChance[(PlayerPrefs.GetInt("Night") - 1) * 3 + clock.index / 2];
        }
        else
        {
            bunnyMoveCheck = 5f;
            bunnyMoveChance = 80;
            teapotMoveCheck = 5f;
            teapotMoveChance = 80;
        }


            //Bunny
            bunnyTimer += Time.deltaTime;
        if (bunnyTimer > bunnyMoveCheck && !gameOver)
        {
            int adjacentRoomIndex = rooms[bunnyIndex].bunnyAdjacentRooms[UnityEngine.Random.Range(0, rooms[bunnyIndex].bunnyAdjacentRooms.Count)];
            if (UnityEngine.Random.Range(0, 100) < bunnyMoveChance)
            {
                transform.position = rooms[adjacentRoomIndex].bunnyPosition;
                bunnyIndex = adjacentRoomIndex;
            }
            bunnyTimer = 0f;
        }

        //Teapot
        teapotTimer += Time.deltaTime;
        if (teapotTimer > teapotMoveCheck && !gameOver)
        {
            int adjacentRoomIndex = rooms[teapotIndex].teapotAdjacentRooms[UnityEngine.Random.Range(0, rooms[teapotIndex].teapotAdjacentRooms.Count)];
            if (UnityEngine.Random.Range(0, 100) < teapotMoveChance)
            {
                teapot.transform.position = rooms[adjacentRoomIndex].teapotPosition;
                teapotIndex = adjacentRoomIndex;
            }
            teapotTimer = 0f;
        }
    }


    public IEnumerator GameOverBunny()
    {
        gameOver = true;
        yield return new WaitForSeconds(0.5f);
        bunnyAudio.PlaySound(0);
        if (bunnyIndex == 5) //Left door
        {
            bunnyOverLeft = true;
            transform.position = new Vector3(-10.89f, 2.33f, 9.02f);
            float jumpScareTimer = 0.5f;
            while (jumpScareTimer > 0f)
            {
                jumpScareTimer -= Time.deltaTime;
                transform.Translate(Vector3.back * (7.15f / 0.5f) * Time.deltaTime); //7.15f
                yield return null;
            }
        }
        else //Right door
        {
            bunnyOverRight = true;
            transform.position = new Vector3(10.89f, 2.33f, 9.02f);
            float jumpScareTimer = 0.5f;
            while (jumpScareTimer > 0f)
            {
                jumpScareTimer -= Time.deltaTime;
                transform.Translate(Vector3.back * (7.15f / 0.5f) * Time.deltaTime);
                yield return null;
            }
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Title Screen");
    }

    public IEnumerator GameOverTeapot()
    {
        gameOver = true;
        yield return new WaitForSeconds(0.5f);
        teapotAudio.PlaySound(0);
        if (teapotIndex == 12) //Left door
        {
            teapotOverLeft = true;
            teapot.transform.position = new Vector3(6.46f, 3.06f, -19.38f);
            float jumpScareTimer = 0.5f;
            while (jumpScareTimer > 0f)
            {
                jumpScareTimer -= Time.deltaTime;
                teapot.transform.Translate(Vector3.forward * (7.15f / 0.5f) * Time.deltaTime); //7.15f
                yield return null;
            }
        }
        else //Right door
        {
            teapotOverRight = true;
            teapot.transform.position = new Vector3(-3.86f, 3.06f, -19.38f);
            float jumpScareTimer = 0.5f;
            while (jumpScareTimer > 0f)
            {
                jumpScareTimer -= Time.deltaTime;
                teapot.transform.Translate(Vector3.forward * (7.15f / 0.5f) * Time.deltaTime);
                yield return null;
            }
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Title Screen");
    }
}

public class Room
{
    public Vector3 bunnyPosition;
    public List<int> bunnyAdjacentRooms;
    public Vector3 teapotPosition;
    public List<int> teapotAdjacentRooms;
    public int nextCam;
    public int prevCam;
    public Material camera;
    public string camNumber;

    public Room()
    {
        bunnyAdjacentRooms = new List<int>();
        teapotAdjacentRooms = new List<int>();
    }
}