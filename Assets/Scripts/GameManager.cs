using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    
    [SerializeField] float spawnFirst = 5;
    [SerializeField] GameObject wallSpawnLocation;
    [SerializeField] GameObject wallKillLocation;
    [SerializeField] GameObject[] wallMeshes;
    [SerializeField] float wallSpeed;
    [SerializeField] float wallSpeedCap;
    [SerializeField] float wallSpeedStep;

    Queue<GameObject> wallQueue = new Queue<GameObject>();

    float timer = 0;

    bool spawnNewWall_b = false;

    int gameScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitandExecute(2));
    }

    IEnumerator WaitandExecute(int seconds)
    {
        yield return new WaitForSeconds(seconds);

        SpawnWall();
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
        int wallIndex = Random.Range(0, wallMeshes.Length);
        //print("spawn wall:" + wallIndex);

        GameObject newWall = Instantiate(wallMeshes[wallIndex], wallSpawnLocation.transform.position, Quaternion.identity);
        Wall wallScript = newWall.AddComponent<Wall>();
        wallScript.SetVariables(wallSpeed);

        wallQueue.Enqueue(newWall);
    }
}
