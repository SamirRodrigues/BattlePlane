using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;


    public void TriggerDialogue()
    {
        try
        {
            DialogueManager.Instance.StartDialogue(dialogue);
        }
        catch
        {
            Debug.LogError("DialogueManager is Null");
        }
    }

    private void Update()
    {   
        if (DialogueManager.Instance != null)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                DialogueManager.Instance.DisolayNextSentence();                
            }

            if (!DialogueManager.Instance.IsDialogueEnded())
            {
                PlayerManager.Instance.SetLock(true);
            }
            else if (DialogueManager.Instance.IsDialogueEnded())
            {
                PlayerManager.Instance.SetLock(false);
            }
        }
    }
}
