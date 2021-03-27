using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportStation : MonoBehaviour
{
    public bool leftTheSafeSpace = false;
    [SerializeField]
    public FlashImage alertFlash;
    public GameObject destroyEffect;

    public DialogueTrigger dTrigger;
    private bool startDialog = false;

    private Player player;

    //Public Dialog

    // Start is called before the first frame update
    void Start()
    {
        dTrigger = GetComponent<DialogueTrigger>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        alertFlash.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (leftTheSafeSpace == true)
        {
            alertFlash.gameObject.SetActive(true);
            alertFlash.StartFlash(1, 0.5f, Color.blue);

            if(Input.GetKey(KeyCode.B))
            {
                player.Heal(50f);
                alertFlash.gameObject.SetActive(false);
                dTrigger.manager.EndDialogue();
                Instantiate(destroyEffect, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }

            if(Input.GetKey(KeyCode.E) && !startDialog)
            {
               startDialog = true;
               dTrigger.TriggerDialogue();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            startDialog = false;
            leftTheSafeSpace = false;
            StartCoroutine(Cooldown(0.5f));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {            
            leftTheSafeSpace = true;            
        }

    }

    IEnumerator Cooldown(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        alertFlash.gameObject.SetActive(false);
    }
}
