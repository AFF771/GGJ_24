using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnWallExit : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if(!other.gameObject.CompareTag("Player")) return;

        other.gameObject.GetComponent<PlayerMouseMove>().RigUnable();
    }
}
