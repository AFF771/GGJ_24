using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerMouseMove : MonoBehaviour
{
    Vector3 mOffset;

    float mZCoord;

    [SerializeField] float xLimit = 2.0f;
    [SerializeField] float yLimitUp = 1.5f;
    [SerializeField] float yLimitDown = 0.0f;

    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] bool controllable = false;

    [SerializeField] GameObject LeftArmTaget;
    [SerializeField] GameObject RightArmTaget;
    [SerializeField] GameObject LeftLegTarget;
    [SerializeField] GameObject RightLegTarget;

    [SerializeField] GameObject Main;
    RigBuilder myRig;

    [SerializeField] GameObject[] tooltips;

    Rigidbody myRb;

    Vector3 worldOffset;

    private GameManager gameManagerRef;

    private bool currentPlayer = false;

    private void Start()
    {
        myRb = GetComponent<Rigidbody>();
        myRig = Main.GetComponent<RigBuilder>();
    }

    // this becomes current player
    public void EnableInput(GameObject manager)
    {
        currentPlayer = true;
        controllable = true;
        gameManagerRef = manager.GetComponent<GameManager>();
    }

    private void Update()
    {
        if (!controllable || !currentPlayer)
        {
            HideTooltips();

            return;
        }

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

            Vector3 targetPosition = GetMouseWorldPos() + worldOffset;

            // check position in bounds
            if ( CheckPositionInBounds(targetPosition) )
            {
                transform.position = targetPosition;
            }
            else
            {
                worldOffset = Vector3.zero;
            }
        }
        else if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                worldOffset = LeftArmTaget.transform.position - GetMouseWorldPos();
                ShowTooltips();
            }

            Vector3 targetPosition = GetMouseWorldPos() + worldOffset;

            myRig.enabled = true;
            // check position in bounds
            if (CheckPositionInBounds(targetPosition))
            {
                LeftArmTaget.transform.position = targetPosition;
            }
            else
            {
                worldOffset = Vector3.zero;
            }
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                worldOffset = RightArmTaget.transform.position - GetMouseWorldPos();
                ShowTooltips();
            }

            Vector3 targetPosition = GetMouseWorldPos() + worldOffset;

            myRig.enabled = true;
            if (CheckPositionInBounds(targetPosition))
            {
                RightArmTaget.transform.position = targetPosition;
            }
            else
            {
                worldOffset = Vector3.zero;
            }
        }
        else if(Input.GetKey(KeyCode.S))
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                worldOffset = LeftLegTarget.transform.position - GetMouseWorldPos();
                ShowTooltips();
            }

            Vector3 targetPosition = GetMouseWorldPos() + worldOffset;

            myRig.enabled = true;
            if (CheckPositionInBounds(targetPosition))
            {
                LeftLegTarget.transform.position = targetPosition;
            }
            else
            {
                worldOffset = Vector3.zero;
            }
        }
        else if(Input.GetKey(KeyCode.A))
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                worldOffset = RightLegTarget.transform.position - GetMouseWorldPos();
                ShowTooltips();
            }

            Vector3 targetPosition = GetMouseWorldPos() + worldOffset;

            myRig.enabled = true;
            if (CheckPositionInBounds(targetPosition))
            {
                RightLegTarget.transform.position = targetPosition;
            }
            else
            {
                worldOffset = Vector3.zero;
            }
        }
        else
        {
            FadeOutTooltips();
            worldOffset = Vector3.zero; ;
        }

    }

    private bool CheckPositionInBounds(Vector3 position)
    {
        return (position.x > -xLimit) && (position.x < xLimit) && (position.y > yLimitDown) && (position.y < yLimitUp);
    }

    private void HideTooltips()
    {
        for (int i = 0; i < tooltips.Length; i++)
        {
            tooltips[i].SetActive(false);
        }
    }

    private void FadeOutTooltips()
    {
        for(int i = 0; i < tooltips.Length; i++)
        {
            tooltips[i].SetActive(true);
            Tooltip tooltipScript = tooltips[i].GetComponent<Tooltip>();
            tooltipScript.FadeOut();
        }
    }

    private void ShowTooltips()
    {
        for (int i = 0; i < tooltips.Length; i++)
        {
            tooltips[i].SetActive(true);
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

    // called when trigger collides with wall
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall") && currentPlayer)
        {
            Debug.Log("Hit wall");

            controllable = false;
            myRb.isKinematic = false;
            myRig.enabled = false;
            myRb.constraints = RigidbodyConstraints.None;
            currentPlayer = false;

            // call kill camera/ trigger next round
            gameManagerRef.TriggerKillCam(this.gameObject);
        }
    }

    public void RigDisable()
    {
        myRig.enabled = false;
    }
}
