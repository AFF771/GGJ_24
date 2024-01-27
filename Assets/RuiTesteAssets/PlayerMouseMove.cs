using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerMouseMove : MonoBehaviour
{
    Vector3 mOffset;

    float mZCoord;

    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] bool controllable;

    [SerializeField] GameObject LeftArmTaget;
    [SerializeField] GameObject RightArmTaget;
    [SerializeField] GameObject LeftLegTarget;
    [SerializeField] GameObject RightLegTarget;

    [SerializeField] GameObject Main;
    RigBuilder myRig;

    [SerializeField] GameObject[] tooltips;

    Rigidbody myRb;

    Vector3 worldOffset;

    private void Start()
    {
        myRb = GetComponent<Rigidbody>();
        controllable = true;
        myRig = Main.GetComponent<RigBuilder>();
    }

    private void Update()
    {
        if (!controllable) return;

        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                worldOffset = transform.position - GetMouseWorldPos();
                ShowTooltips();
            }

            transform.position = GetMouseWorldPos() + worldOffset;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                worldOffset = LeftArmTaget.transform.position - GetMouseWorldPos();
                ShowTooltips();
            }

            myRig.enabled = true;
            LeftArmTaget.transform.position = GetMouseWorldPos() + worldOffset;
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                worldOffset = RightArmTaget.transform.position - GetMouseWorldPos();
                ShowTooltips();
            }
            myRig.enabled = true;
            RightArmTaget.transform.position = GetMouseWorldPos() + worldOffset;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                worldOffset = LeftLegTarget.transform.position - GetMouseWorldPos();
                ShowTooltips();
            }
            myRig.enabled = true;
            LeftLegTarget.transform.position = GetMouseWorldPos() + worldOffset;
        }
        else if(Input.GetKey(KeyCode.A))
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                worldOffset = RightLegTarget.transform.position - GetMouseWorldPos();
                ShowTooltips();
            }
            myRig.enabled = true;
            RightLegTarget.transform.position = GetMouseWorldPos() + worldOffset;
        }
        else
        {
            FadeOutTooltips();
        }

    }

    private void FadeOutTooltips()
    {
        for(int i = 0; i < tooltips.Length; i++)
        {
            Tooltip tooltipScript = tooltips[i].GetComponent<Tooltip>();
            tooltipScript.FadeOut();
        }
    }

    private void ShowTooltips()
    {
        for (int i = 0; i < tooltips.Length; i++)
        {
            Tooltip tooltipScript = tooltips[i].GetComponent<Tooltip>();
            tooltipScript.ShowTooltip();
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Wall")) return;

        myRb.isKinematic = false;
        myRig.enabled = false;
        myRb.constraints = RigidbodyConstraints.None;
        controllable = false;
    }

    public void RigUnable()
    {
        myRig.enabled = false;
    }
}
