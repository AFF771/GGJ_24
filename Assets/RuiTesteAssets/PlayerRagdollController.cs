using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRagdollController : MonoBehaviour
{
    [SerializeField] GameObject[] limbs;
    [SerializeField] bool toggleRigdollTeste = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (toggleRigdollTeste)
            ToggleRigdoll();
    }

    public void ToggleRigdoll()
    {
        for(int i = 0; i < limbs.Length; i++)
        {
            limbs[i].GetComponent<Rigidbody>().isKinematic = false;
        }
    }

}
