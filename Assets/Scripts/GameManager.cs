using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] AudioSource[] audioSources;
    
    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] Canvas restartScreen;

    [SerializeField] GameObject camera_h;

    [SerializeField] float killCamDuration;

    [SerializeField] GameObject playerPrefab;
    [SerializeField] Vector3 playerPosition;
    [SerializeField] Vector3 playerRotation;

    [Header("Walls")]
    [SerializeField] float spawnFirst = 5;
    [SerializeField] GameObject wallSpawnLocation;
    [SerializeField] GameObject wallKillLocation;
    [SerializeField] GameObject[] wallMeshes;
    [SerializeField] float wallSpeed;
    [SerializeField] float wallSpeedCap;
    [SerializeField] float wallSpeedStep;
    [SerializeField] GameObject[] curtains;

    GameObject player;
    PlayerMouseMove movementScript;

    Animator leftCurtainAnim;
    Animator rightCurtainAnim;

    Queue<GameObject> wallQueue = new Queue<GameObject>();

    private CameraHandler cameraHandlerRef;

    private float timer = 0;

    private bool spawnNewWall_b = false;

    private int gameScore = 0;

    private float startWallSpeed;

    private void Awake()
    {
        restartScreen.enabled = false;
        startWallSpeed = wallSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        leftCurtainAnim = curtains[0].GetComponent<Animator>();
        rightCurtainAnim = curtains[1].GetComponent<Animator>();

        cameraHandlerRef = camera_h.GetComponent<CameraHandler>();    

        player = Instantiate(playerPrefab, playerPosition, Quaternion.Euler(playerRotation));

        StartCoroutine(WaitandExecute(3));
    }

    // wait for initial camera animation
    IEnumerator WaitandExecute(int seconds)
    {
        yield return new WaitForSeconds(seconds);

        SpawnWall();
        movementScript = player.GetComponentInChildren<PlayerMouseMove>();
        movementScript.EnableInput(gameObject);
        Debug.Log("ENABLE INPUT");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        scoreText.text = gameScore.ToString();

        if (wallQueue != null)
        {
            
            foreach (GameObject wall in wallQueue)
            {
                //Debug.Log(wall.transform.position.z);
                if (wall.transform.position.z < wallKillLocation.transform.position.z)
                {
                    spawnNewWall_b = true;


                }
            }

            if (spawnNewWall_b)
            {
                spawnNewWall_b = false;

                wallSpeed += wallSpeedStep;

                GameObject lastWall = wallQueue.Peek();
                Destroy(lastWall);
                wallQueue.Dequeue();
                SpawnWall();

                gameScore += 100;
            }
        }

    }

    // Spawn new wall
    void SpawnWall()
    {
        leftCurtainAnim.SetTrigger("trigger");
        rightCurtainAnim.SetTrigger("trigger");

        int wallIndex = Random.Range(0, wallMeshes.Length - 1);
        //print("spawn wall:" + wallIndex);

        GameObject newWall = Instantiate(wallMeshes[wallIndex], wallSpawnLocation.transform.position, Quaternion.identity);
        Wall wallScript = newWall.AddComponent<Wall>();
        wallScript.SetVariables(wallSpeed, wallSpeedCap, camera_h);

        wallQueue.Enqueue(newWall);
    }

    // called when player colides whith wall
    public void TriggerKillCam(GameObject playerGameObj)
    {
        audioSources[0].Play();

        // kill cam (follow player)
        cameraHandlerRef.KillCam(playerGameObj, killCamDuration);

        StartCoroutine(WaitForNewRound());
    }

    IEnumerator WaitForNewRound()
    {
        yield return new WaitForSeconds(killCamDuration);

        // RETRY SCREEN
        restartScreen.enabled = true;
        Time.timeScale = 0;
    }

    public void NewRound()
    {
        if (!restartScreen.enabled) return;
        
        restartScreen.enabled = false;
        Time.timeScale = 1;

        wallSpeed = startWallSpeed;
        gameScore = 0;
        
        // do changes to dumy before losing reference
        
        // spawn new player
        player = Instantiate(playerPrefab, playerPosition, Quaternion.Euler(playerRotation));
        movementScript = player.GetComponentInChildren<PlayerMouseMove>();
        movementScript.EnableInput(gameObject);

        Debug.Log("New Round");

        // change back camera
    }
}
