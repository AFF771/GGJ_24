using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    private float alpha = 0;
    private float timeVisible = 3;
    private float fadeOutTime = 2;

    SpriteRenderer spriteRenderer = null;

    bool interruptLerp = false;
    bool lerping = false;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ShowTooltip()
    {
        interruptLerp = true;
        StartCoroutine(ShowTooltipTimer());
    }

    public void FadeOut()
    {
        if (!lerping)
        {
            StartCoroutine(LerpOpacity());
        }
    }

    IEnumerator ShowTooltipTimer()
    {
        yield return new WaitForSeconds(timeVisible);
        LerpOpacity();
    }

    IEnumerator LerpOpacity()
    {          
        float elapsedTime = 0f;
        Color startColor = spriteRenderer.material.color;
        Color targetColor = new Color(1.0f, 1.0f, 1.0f, 0);
        lerping = true;

        while (elapsedTime < fadeOutTime)
        {
            if (interruptLerp)
            {
                spriteRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                interruptLerp = false;
                elapsedTime = fadeOutTime;

                yield return null;
            }
            else
            {
                spriteRenderer.material.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeOutTime);
                elapsedTime += Time.deltaTime;

                yield return null;
            }                  
        }

        lerping = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
    }
}
