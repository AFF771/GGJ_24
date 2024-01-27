using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float force;
    [SerializeField] float jumpForce;
    [SerializeField] bool lost = false;
    Rigidbody myRb;
    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lost) return;

        Vector3 horizontalMov = new Vector3 (Input.GetAxis("Horizontal"), 0,0);

        myRb.AddForce(horizontalMov * force );

        if (Input.GetButtonDown("Jump"))
        {
            myRb.AddForce(new Vector3(0, jumpForce, 0));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Wall"))
        {
            myRb.constraints = RigidbodyConstraints.None;
            lost = true;
        }
    }
}
