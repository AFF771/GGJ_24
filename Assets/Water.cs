using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] AudioSource[] audioSources;

    // Start is called before the first frame update
    void Start()
    {
}

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHit"))
        {
            int splashIndex = Random.Range(0, 2);

            audioSources[splashIndex].Play();

        }
    }
}
