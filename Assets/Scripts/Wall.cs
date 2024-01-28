using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] float fadeStartDistance = 1.0f;
    
    private float speed;
    private GameObject cameraRef;

    private Renderer wallRenderer;
    private Color initialColor;

    // Start is called before the first frame update
    void Start()
    {
        wallRenderer = GetComponent<Renderer>();
        initialColor = wallRenderer.material.color;
    }

    public void SetVariables(float wallSpeed, float speedCap, GameObject camera)
    {
        speed = wallSpeed;
        cameraRef = camera;

        if (speed > speedCap)
        {
            speed = speedCap;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 0, -speed * Time.deltaTime);

        float  distanceToCamera = Vector3.Distance(gameObject.transform.position, cameraRef.transform.position);

        if (distanceToCamera < fadeStartDistance )
        {
            float mappedOpacity = Mathf.InverseLerp(0.0f, fadeStartDistance, distanceToCamera);
            float newOpacity = Mathf.Lerp(1.0f, mappedOpacity, Time.deltaTime * 5.0f);

            Color newColor = new Color(initialColor.r, initialColor.g, initialColor.b, newOpacity);
            wallRenderer.material.color = newColor;
        }
    }
}