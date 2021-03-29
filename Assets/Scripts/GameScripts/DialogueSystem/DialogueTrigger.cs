using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public DialogueManager manager;

    private Player player;

    public void TriggerDialogue()
    {        
        if (manager != null)
        {
            manager.StartDialogue(dialogue);
        }
        else
        {
            manager = FindObjectOfType<DialogueManager>();
            TriggerDialogue();
        }
    }

    private void Start()
    {
        manager = FindObjectOfType<DialogueManager>();
        player = GameObject.FindObjectOfType<Player>();     
    }

    

    private void Update()
    {   
        if (manager != null)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {                
                manager.DisolayNextSentence();                
            }

            if (!manager.IsDialogueEnded())
            {
                player.SetLock(true);
            }
            else if (manager.IsDialogueEnded())
            {
                player.SetLock(false);
            }
        }
    }
}
