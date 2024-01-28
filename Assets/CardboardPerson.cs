using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardboardPerson : MonoBehaviour
{
    [SerializeField] float minHeight = -0.5f;
    [SerializeField] float maxHeight = 0.5f;
    [SerializeField] float minSpeed = 1.0f;
    [SerializeField] float maxSpeed = 5.0f;

    float targetHeight;
    float speed;
    Vector3 startPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        SetRandomHeight();
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, targetHeight, transform.position.z), step);

        if (Vector3.Distance(transform.position, new Vector3(transform.position.x, targetHeight, transform.position.z)) < 0.1f)
        {
            SetRandomHeight();
        }
    }

    void SetRandomHeight()
    {
        targetHeight = startPosition.y + Random.Range(minHeight, maxHeight);
        speed = Random.Range(minSpeed, maxSpeed);
    }
}
