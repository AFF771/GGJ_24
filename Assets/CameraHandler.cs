using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] Transform gameTransform;
    [SerializeField] float transitionDuration = 1.0f;

    private Vector3 startingPosition;
    private Quaternion startingRotation;
    private float elapsedTime = 0.0f;

    private bool initialCameraMovement = true;
    private bool followPlayer = false;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        startingRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (initialCameraMovement)
        {
            float t = Mathf.Clamp01(elapsedTime / transitionDuration);

            transform.position = Vector3.Lerp(startingPosition, gameTransform.position, t);
            transform.rotation = Quaternion.Slerp(startingRotation, gameTransform.rotation, t);

            if (t >= transitionDuration)
            {
                elapsedTime = 0.0f;
                initialCameraMovement = false;
            }
        }

        if (followPlayer && player)
        {
            gameObject.transform.LookAt(player.transform.position);
        }

    }

    public void KillCam(GameObject playerDummy, float killCamDuration)
    {
        Debug.Log("Kill Cam active");
        player = playerDummy;
        followPlayer = true;
        StartCoroutine(WaitKillCam(killCamDuration));

    }

    IEnumerator WaitKillCam(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        gameObject.transform.rotation = gameTransform.transform.rotation;
        player = null;
        followPlayer = false;
    }
}
