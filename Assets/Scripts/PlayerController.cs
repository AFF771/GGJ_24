using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float force;
    [SerializeField] float jumpForce;
    [SerializeField] bool lost = false;
    Rigidbody myRb;

    [SerializeField] bool ground = false;
    [SerializeField] float rotationSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("update player controller");

        if (lost) return;

        Vector3 horizontalMov = new Vector3 (Input.GetAxis("Horizontal"), 0,0);

        myRb.AddForce(horizontalMov * force );

        if (Input.GetButtonDown("Jump"))
        {
            myRb.AddForce(new Vector3(0, jumpForce, 0));
            ground = false;
        }

        if (Input.GetButton("Fire1"))
        {
            transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
        }

        if (Input.GetButton("Fire2"))
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Wall"))
        {
            Debug.Log("hit");
            myRb.constraints = RigidbodyConstraints.None;
            lost = true;
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("ground hit");
            ground = true;
        }
    }
}