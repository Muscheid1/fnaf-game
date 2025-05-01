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

    private Fade fade;

    public GameObject leftDoorState;
    public GameObject rightDoorState;
    public GameObject toggleDoorState;

    public AudioManager audioManager;

    private bool powerDifficultySet = false;

    MultiChannelAudio headAudio;

    [System.Serializable]
    public class JumpscareMoment
    {
        public Transform transform;
        public float time;
    }

    public JumpscareMoment[] jumpscareMomentsBunnyLeft;
    public JumpscareMoment[] jumpscareMomentsBunnyRight;
    public JumpscareMoment[] jumpscareMomentsTeapotLeft;
    public JumpscareMoment[] jumpscareMomentsTeapotRight;

    public GameObject[] powerDeathLights;

    void Start()
    {
        fade = GameObject.Find("DeathImage").GetComponent<Fade>();

        powerState = GameObject.Find("power-display").GetComponent<Power>();

        bunnyAudio = GetComponent<MultiChannelAudio>();
        teapotAudio = GameObject.Find("teapot").GetComponent<MultiChannelAudio>();
        headAudio =  GameObject.Find("HeadAudio").GetComponent<MultiChannelAudio>();

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
        if (clock.victory) //No deaths after win
        {
            return;
        }

        if (!powerState.powerOff)
        {
            bunnyMoveCheck = GameDifficulty.bunnyMoveCheck[(PlayerPrefs.GetInt("Night") - 1) * 3 + clock.index / 2];
            bunnyMoveChance = GameDifficulty.bunnyMoveChance[(PlayerPrefs.GetInt("Night") - 1) * 3 + clock.index / 2];
            teapotMoveCheck = GameDifficulty.teapotMoveCheck[(PlayerPrefs.GetInt("Night") - 1) * 3 + clock.index / 2];
            teapotMoveChance = GameDifficulty.teapotMoveChance[(PlayerPrefs.GetInt("Night") - 1) * 3 + clock.index / 2];
        }
        else
        {
            if (!powerDifficultySet)
            {
                powerDifficultySet = true;
                StartCoroutine(PowerDifficulty());
            }
        }


        //Bunny
        bunnyTimer += Time.deltaTime;
        if ((bunnyTimer > bunnyMoveCheck || (bunnyTimer > 4f && (bunnyIndex == 5 || bunnyIndex == 6))) && !gameOver)
        {
            int adjacentRoomIndex = rooms[bunnyIndex].bunnyAdjacentRooms[UnityEngine.Random.Range(0, rooms[bunnyIndex].bunnyAdjacentRooms.Count)];
            if (UnityEngine.Random.Range(0, 100) < bunnyMoveChance || (bunnyIndex == 5 || bunnyIndex == 6))
            {
                transform.position = rooms[adjacentRoomIndex].bunnyTransform.position;
                transform.rotation = rooms[adjacentRoomIndex].bunnyTransform.rotation;
                bunnyIndex = adjacentRoomIndex;
                if (bunnyIndex != 5 && bunnyIndex != 6)
                {
                    headAudio.PlaySound(1);
                }
            }
            bunnyTimer = 0f;
        }
        if ((bunnyIndex == 5 || bunnyIndex == 6) && !bunnyAtDoor) //Sound of bunny at door
        {
            if (!(leftDoorState.GetComponent<Door>().open && bunnyIndex == 5) && !(rightDoorState.GetComponent<Door>().open && bunnyIndex == 6))
            {
                bunnyDoorNoise = bunnyAudio.PlaySound(1);
            }
            bunnyAtDoor = true;
        }
        if (bunnyIndex != 5 && bunnyIndex != 6 && bunnyAtDoor) //Bunny leaves doors
        {
            Destroy(bunnyDoorNoise);
            bunnyAtDoor = false;
        }

        //Teapot
        teapotTimer += Time.deltaTime;
        if ((teapotTimer > teapotMoveCheck || (teapotTimer > 4f && (teapotIndex == 11 || teapotIndex == 12))) && !gameOver)
        {
            int adjacentRoomIndex = rooms[teapotIndex].teapotAdjacentRooms[UnityEngine.Random.Range(0, rooms[teapotIndex].teapotAdjacentRooms.Count)];
            if (UnityEngine.Random.Range(0, 100) < teapotMoveChance || (teapotIndex == 11 || teapotIndex == 12))
            {
                teapot.transform.position = rooms[adjacentRoomIndex].teapotTransform.position;
                teapot.transform.rotation = rooms[adjacentRoomIndex].teapotTransform.rotation;
                teapotIndex = adjacentRoomIndex;
                if (teapotIndex != 11 && teapotIndex != 12)
                {
                    headAudio.PlaySound(2);
                }
            }
            teapotTimer = 0f;
        }
        if ((teapotIndex == 11 || teapotIndex == 12) && !teapotAtDoor) //Sound of teapot at door
        {
            if (!(toggleDoorState.GetComponent<TogglerDoor>().open && teapotIndex == 12) && !(!toggleDoorState.GetComponent<TogglerDoor>().open && teapotIndex == 11))
            {
                teapotDoorNoise = teapotAudio.PlaySound(1);
            }
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
        yield return new WaitForSeconds(0.3f);
        bunnyAudio.PlaySound(0);
        if (bunnyIndex == 5) //Left door
        {
            if (powerState.powerOff)
            {
                powerDeathLights[0].SetActive(true);
            }
            bunnyOverLeft = true;
        }
        else //Right door
        {
            if (powerState.powerOff)
            {
                powerDeathLights[1].SetActive(true);
            }
            bunnyOverRight = true;
        }
        for (int i = 0; i < 2; i++)
        {
            float jumpScareTimer = 0f;
            while (jumpScareTimer < jumpscareMomentsBunnyLeft[i + 1].time)
            {
                jumpScareTimer += Time.deltaTime;
                float t = jumpScareTimer / jumpscareMomentsBunnyLeft[i + 1].time;
                transform.position = bunnyOverLeft ? Vector3.Lerp(jumpscareMomentsBunnyLeft[i].transform.position, jumpscareMomentsBunnyLeft[i+1].transform.position, t) : Vector3.Lerp(jumpscareMomentsBunnyRight[i].transform.position, jumpscareMomentsBunnyRight[i+1].transform.position, t);
                transform.rotation = bunnyOverLeft ? Quaternion.Lerp(jumpscareMomentsBunnyLeft[i].transform.rotation, jumpscareMomentsBunnyLeft[i+1].transform.rotation, t) : Quaternion.Lerp(jumpscareMomentsBunnyRight[i].transform.rotation, jumpscareMomentsBunnyRight[i+1].transform.rotation, t);
                yield return null;
            }
        }

        yield return new WaitForSeconds(0.35f);
        fade.FadeToBlack();
        audioManager.SetVolume(-80f, "Ambience");
        audioManager.SetVolume(-80f, "Effects");
        headAudio.PlaySound(0);
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("Title Screen");
    }

    public IEnumerator GameOverTeapot()
    {
        gameOver = true;
        yield return new WaitForSeconds(0.3f);
        teapotAudio.PlaySound(0);
        if (teapotIndex == 12) //Left door
        {
            if (powerState.powerOff)
            {
                powerDeathLights[2].SetActive(true);
            }
            teapotOverLeft = true;
        }
        else //Right door
        {
            if (powerState.powerOff)
            {
                powerDeathLights[3].SetActive(true);
            }
            teapotOverRight = true;
        }
        for (int i = 0; i < 2; i++)
        {
            float jumpScareTimer = 0f;
            while (jumpScareTimer < jumpscareMomentsTeapotLeft[i + 1].time)
            {
                jumpScareTimer += Time.deltaTime;
                float t = jumpScareTimer / jumpscareMomentsTeapotLeft[i + 1].time;
                teapot.transform.position = teapotOverLeft ? Vector3.Lerp(jumpscareMomentsTeapotLeft[i].transform.position, jumpscareMomentsTeapotLeft[i + 1].transform.position, t) : Vector3.Lerp(jumpscareMomentsTeapotRight[i].transform.position, jumpscareMomentsTeapotRight[i + 1].transform.position, t);
                teapot.transform.rotation = teapotOverLeft ? Quaternion.Lerp(jumpscareMomentsTeapotLeft[i].transform.rotation, jumpscareMomentsTeapotLeft[i + 1].transform.rotation, t) : Quaternion.Lerp(jumpscareMomentsTeapotRight[i].transform.rotation, jumpscareMomentsTeapotRight[i + 1].transform.rotation, t);
                yield return null;
            }
        }


        yield return new WaitForSeconds(0.35f);
        fade.FadeToBlack();
        audioManager.SetVolume(-80f, "Ambience");
        audioManager.SetVolume(-80f, "Effects");
        headAudio.PlaySound(0);
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("Title Screen");
    }


    private IEnumerator PowerDifficulty()
    {
        yield return new WaitForSeconds(6f);
        bunnyMoveCheck = 5f;
        bunnyMoveChance = 70;
        teapotMoveCheck = 4f;
        teapotMoveChance = 80;
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