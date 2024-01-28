using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRandomConfig : MonoBehaviour
{
    [SerializeField] Material[] safeMaterials;
    [SerializeField] Material[] gearMaterials;
    [SerializeField] Material[] faceMaterials;

    SkinnedMeshRenderer myRenderer;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<SkinnedMeshRenderer>();

        // Check if the mesh has at least 3 materials
        if (myRenderer.materials.Length >= 3)
        {
            Material[] currentMaterials = myRenderer.materials;
                
            // Randomly choose materials for safe and gear slots
            Material randomSafeMaterial = safeMaterials[Random.Range(0, safeMaterials.Length)];
            Material randomGearMaterial = gearMaterials[Random.Range(0, gearMaterials.Length)];
            Material randomFace = faceMaterials[Random.Range(0, faceMaterials.Length)];

            // Assign the randomly chosen materials
            currentMaterials[1] = randomSafeMaterial; // Assuming index 1 is for safe
            currentMaterials[2]= randomGearMaterial; // Assuming index 2 is for gear
            currentMaterials[0] = randomFace;

            myRenderer.materials = currentMaterials;
        }
        else
        {
            Debug.LogError("The renderer does not have enough materials for assignment.");
        }
    }
}
