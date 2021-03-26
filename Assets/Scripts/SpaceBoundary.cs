using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBoundary : MonoBehaviour
{
    public bool leftTheSafeSpace = false;
    public FlashImage alertFlash;

    // Start is called before the first frame update
    void Start()
    {
        alertFlash.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(leftTheSafeSpace == true)
        {
            alertFlash.gameObject.SetActive(true);
            alertFlash.StartFlash(1, 0.5f, Color.red);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            leftTheSafeSpace = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            leftTheSafeSpace = false;
            StartCoroutine(Cooldown(0.5f));
        }
        
    }

    IEnumerator Cooldown(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        alertFlash.gameObject.SetActive(false);
    }
   
}
