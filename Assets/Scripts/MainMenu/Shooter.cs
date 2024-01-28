using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject targetPrefab;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] float forceMagnitude = 10f; // Adjust this value based on the desired shooting force

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Shoot", Random.Range(0f, 15f), Random.Range(0f, 15f));
    }

    void Shoot()
    {
        // Instantiate a new player object
        GameObject player = Instantiate(playerPrefab, transform.position, Quaternion.identity);

        // Shoot the new player towards the target direction
        Vector3 direction = (targetPrefab.transform.position - player.transform.position).normalized;

        // Apply force to the player rigidbodies (assuming they have one)
        Rigidbody[] playerRigidbodies = player.GetComponentsInChildren<Rigidbody>();
        if (playerRigidbodies != null && playerRigidbodies.Length > 0)
        {
            foreach (Rigidbody rb in playerRigidbodies)
            {
                rb.AddForce(direction * forceMagnitude, ForceMode.Impulse);
            }
        }
    }
}
