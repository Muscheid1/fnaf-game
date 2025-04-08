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

    public Transform[] bunnyTransforms;
    public Transform[] teapotTransforms;

    public bool bunnyOverLeft = false;
    public bool bunnyOverRight = false;
    public bool teapotOverLeft = false;
    public bool teapotOverRight = false;

    public bool gameOver = false;

    MultiChannelAudio bunnyAudio;
    MultiChannelAudio teapotAudio;
    private Power powerState;

    private GameObject bunnyDoorNoise;
    private bool bunnyAtDoor = false;
    private GameObject teapotDoorNoise;
    private bool teapotAtDoor = false;

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

        room.bunnyTransform = bunnyTransforms[0];
        room.bunnyAdjacentRooms.Add(1);
        room.bunnyAdjacentRooms.Add(2);

        room.teapotTransform = teapotTransforms[0];
        room.teapotAdjacentRooms.Add(1);
        room.teapotAdjacentRooms.Add(2);

        room.prevCam = 10;
        room.nextCam = 1;
        room.camera = cameras[0];
        room.camNumber = "1";
        rooms.Add(room);

        //Index 1
        room = new Room();

        room.bunnyTransform = bunnyTransforms[1];
        room.bunnyAdjacentRooms.Add(2);
        room.bunnyAdjacentRooms.Add(3);

        room.teapotTransform = teapotTransforms[1];
        room.teapotAdjacentRooms.Add(2);
        room.teapotAdjacentRooms.Add(7);

        room.prevCam = 0;
        room.nextCam = 2;
        room.camera = cameras[1];
        room.camNumber = "2";
        rooms.Add(room);

        //Index 2
        room = new Room();

        room.bunnyTransform = bunnyTransforms[2];
        room.bunnyAdjacentRooms.Add(1);
        room.bunnyAdjacentRooms.Add(4);

        room.teapotTransform = teapotTransforms[2];
        room.teapotAdjacentRooms.Add(1);
        room.teapotAdjacentRooms.Add(8);

        room.prevCam = 1;
        room.nextCam = 3;
        room.camera = cameras[2];
        room.camNumber = "3";
        rooms.Add(room);

        //Index 3
        room = new Room();

        room.bunnyTransform = bunnyTransforms[3];
        room.bunnyAdjacentRooms.Add(4);
        room.bunnyAdjacentRooms.Add(5);

        room.prevCam = 2;
        room.nextCam = 4;
        room.camera = cameras[3];
        room.camNumber = "4";
        rooms.Add(room);

        //Index 4
        room = new Room();
        room.bunnyTransform = bunnyTransforms[4];
        room.bunnyAdjacentRooms.Add(3);
        room.bunnyAdjacentRooms.Add(6);
        room.prevCam = 3;
        room.nextCam = 7;
        room.camera = cameras[4];
        room.camNumber = "5";
        rooms.Add(room);

        //Index 5
        room = new Room();
        room.bunnyTransform = bunnyTransforms[5];
        room.bunnyAdjacentRooms.Add(0);
        rooms.Add(room);

        //Index 6
        room = new Room();
        room.bunnyTransform = bunnyTransforms[6];
        room.bunnyAdjacentRooms.Add(0);
        rooms.Add(room);

        //Index 7
        room = new Room();

        room.teapotTransform = teapotTransforms[5];
        room.teapotAdjacentRooms.Add(9);

        room.prevCam = 4;
        room.nextCam = 8;
        room.camera = cameras[5];
        room.camNumber = "6";
        rooms.Add(room);

        //Index 8
        room = new Room();

        room.teapotTransform = teapotTransforms[6];
        room.teapotAdjacentRooms.Add(10);

        room.prevCam = 7;
        room.nextCam = 9;
        room.camera = cameras[6];
        room.camNumber = "7";
        rooms.Add(room);

        //Index 9
        room = new Room();

        room.teapotTransform = teapotTransforms[7];
        room.teapotAdjacentRooms.Add(11);

        room.prevCam = 8;
        room.nextCam = 10;
        room.camera = cameras[7];
        room.camNumber = "8";
        rooms.Add(room);

        //Index 10
        room = new Room();

        room.teapotTransform = teapotTransforms[8];
        room.teapotAdjacentRooms.Add(12);

        room.prevCam = 9;
        room.nextCam = 0;
        room.camera = cameras[8];
        room.camNumber = "9";
        rooms.Add(room);

        //Index 11
        room = new Room();
        room.teapotTransform = teapotTransforms[9];
        room.teapotAdjacentRooms.Add(0);
        rooms.Add(room);

        //Index 12
        room = new Room();
        room.teapotTransform = teapotTransforms[10];
        room.teapotAdjacentRooms.Add(0);
        rooms.Add(room);
    }

    private void Awake()
    {
        teapot = GameObject.Find("teapot");
        transform.position = bunnyTransforms[0].position;
        transform.rotation = bunnyTransforms[0].rotation;
        teapot.transform.position = teapotTransforms[0].position;
        teapot.transform.rotation = teapotTransforms[0].rotation;

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
                transform.position = rooms[adjacentRoomIndex].bunnyTransform.position;
                transform.rotation = rooms[adjacentRoomIndex].bunnyTransform.rotation;
                bunnyIndex = adjacentRoomIndex;
            }
            bunnyTimer = 0f;
        }
        if ((bunnyIndex == 5 || bunnyIndex == 6) && !bunnyAtDoor) //Sound of bunny at door
        {
            bunnyDoorNoise = bunnyAudio.PlaySound(1);
            bunnyAtDoor = true;
        }
        if (bunnyIndex != 5 && bunnyIndex != 6 && bunnyAtDoor) //Bunny leaves doors
        {
            Destroy(bunnyDoorNoise);
            bunnyAtDoor = false;
        }

        //Teapot
        teapotTimer += Time.deltaTime;
        if (teapotTimer > teapotMoveCheck && !gameOver)
        {
            int adjacentRoomIndex = rooms[teapotIndex].teapotAdjacentRooms[UnityEngine.Random.Range(0, rooms[teapotIndex].teapotAdjacentRooms.Count)];
            if (UnityEngine.Random.Range(0, 100) < teapotMoveChance)
            {
                teapot.transform.position = rooms[adjacentRoomIndex].teapotTransform.position;
                teapot.transform.rotation = rooms[adjacentRoomIndex].teapotTransform.rotation;
                teapotIndex = adjacentRoomIndex;
            }
            teapotTimer = 0f;
        }
        if ((teapotIndex == 11 || teapotIndex == 12) && !teapotAtDoor) //Sound of teapot at door
        {
            teapotDoorNoise = teapotAudio.PlaySound(1);
            teapotAtDoor = true;
        }
        if (teapotIndex != 11 && teapotIndex != 12 && teapotAtDoor) //Teapot leaves doors
        {
            Destroy(teapotDoorNoise);
            teapotAtDoor = false;
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
            float jumpScareTimer = 0f;
            while (jumpScareTimer < 0.5f)
            {
                float t = jumpScareTimer / 0.5f;
                jumpScareTimer += Time.deltaTime;
                transform.position = Vector3.Lerp(bunnyTransforms[5].position, bunnyTransforms[7].position, t);
                yield return null;
            }
        }
        else //Right door
        {
            bunnyOverRight = true;

            float jumpScareTimer = 0f;
            while (jumpScareTimer < 0.5f)
            {
                float t = jumpScareTimer / 0.5f;
                jumpScareTimer += Time.deltaTime;
                transform.position = Vector3.Lerp(bunnyTransforms[6].position, bunnyTransforms[8].position, t);
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
            float jumpScareTimer = 0f;
            while (jumpScareTimer < 0.5f)
            {
                float t = jumpScareTimer / 0.5f;
                jumpScareTimer += Time.deltaTime;
                teapot.transform.position = Vector3.Lerp(teapotTransforms[10].position, teapotTransforms[12].position, t);
                yield return null;
            }
        }
        else //Right door
        {
            teapotOverRight = true;
            float jumpScareTimer = 0f;
            while (jumpScareTimer < 0.5f)
            {
                float t = jumpScareTimer / 0.5f;
                jumpScareTimer += Time.deltaTime;
                teapot.transform.position = Vector3.Lerp(teapotTransforms[9].position, teapotTransforms[11].position, t);
                yield return null;
            }
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Title Screen");
    }
}

public class Room
{
    public Transform bunnyTransform;
    public List<int> bunnyAdjacentRooms;
    public Transform teapotTransform;
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