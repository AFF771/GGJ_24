using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class Wall : MonoBehaviour
{
    float speed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetVariables(float wallSpeed)
    {
        speed = wallSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 0, -speed * Time.deltaTime);
    }
}