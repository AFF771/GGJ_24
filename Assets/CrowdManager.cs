using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdManager : MonoBehaviour
{
    [SerializeField] GameObject cardboardPerson;

    [SerializeField] float peoplePerRow;
    [SerializeField] float spaceBetween;

    [SerializeField] GameObject[] rowsPositions;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < rowsPositions.Length; i++)
        {
            SpawnRow(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnRow(int rowIndex)
    {
        Vector3 spawnLocation = rowsPositions[rowIndex].transform.position;
        Vector3 spawnRotation = new Vector3(0.0f, (spawnLocation.x > 0 ? 0.0f : 180.0f),0.0f);
        Vector3 step = new Vector3(0.0f, 0.0f, spaceBetween);
        
        for(int i = 0; i < peoplePerRow; i++)
        {
            GameObject newCrowd = Instantiate(cardboardPerson, spawnLocation - (step * i), Quaternion.Euler(spawnRotation));
            newCrowd.transform.parent = transform;
        }
    }
}
