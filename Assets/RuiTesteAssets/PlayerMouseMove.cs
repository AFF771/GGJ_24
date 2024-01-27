using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseMove : MonoBehaviour
{
    Vector3 mOffset;

    float mZCoord;
    float mYCoord;

    private void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        mYCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).y;

        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;
        mousePoint.y = mYCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        //if (Input.GetKey() || Input.anyKey != Input.GetMouseButton(0)) return;
        Debug.Log("123");
        transform.position = GetMouseWorldPos() + mOffset;
    }
}
