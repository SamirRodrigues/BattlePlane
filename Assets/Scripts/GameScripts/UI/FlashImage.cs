using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Image))]
public class FlashImage : MonoBehaviour
{
    Image image = null;
    Coroutine currentFlashRoutine = null;
    private bool endFlash = true;


    private void Awake()
    {
        image = GetComponent<Image>();
    }

    // StartFlash
    public void StartFlash(float secondsForOneFlash, float maxAlpha, Color newColor)
    {
        image.color = newColor;

        //ensure maxAlpha isn't above 1

        maxAlpha = Mathf.Clamp(maxAlpha, 0, 1);
        if(endFlash == true)
        {
            endFlash = false;
            if (currentFlashRoutine != null)
            {
                StopCoroutine(currentFlashRoutine);
            }
            currentFlashRoutine = StartCoroutine(Flash(secondsForOneFlash, maxAlpha));
        }        
    }

    IEnumerator Flash(float secondsForOneFlash, float maxAlpha)
    {
        float flashDuration = secondsForOneFlash / 2;

        //FlashIn
        for (float t = 0; t <= flashDuration; t += Time.deltaTime)
        {
            Color colorThisFrame = image.color;
            colorThisFrame.a = Mathf.Lerp(0, maxAlpha, t/flashDuration);
            image.color = colorThisFrame;
            yield return null; //Wait until the next frame
        }
        
        //FlashOut
        for (float t = 0; t <= flashDuration; t += Time.deltaTime)
        {
            Color colorThisFrame = image.color;
            colorThisFrame.a = Mathf.Lerp(maxAlpha, 0, t/flashDuration);
            image.color = colorThisFrame;
            yield return null;
        }

        // Ensure alpha is set to 0

        image.color = new Color32(0, 0, 0, 0); // Using color32 to make sure then catch alpha
        endFlash = true;
    }
}
