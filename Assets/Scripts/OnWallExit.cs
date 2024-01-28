using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class OnWallExit : MonoBehaviour
{
    private bool hit = false;
    
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("PlayerHit") && !hit)
        {
            hit = true;
            Debug.Log("Wall exit reset rig");
            other.gameObject.GetComponentInParent<RigBuilder>().enabled = false;
        }
    }
}
