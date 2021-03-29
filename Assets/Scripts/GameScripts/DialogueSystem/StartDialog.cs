using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialog : MonoBehaviour
{
    public DialogueTrigger trigger;
    // Start is called before the first frame update

    private bool start = false;

    void Start()
    {
        start = false;
        trigger = GetComponent<DialogueTrigger>();
    }

    private void Update()
    {
        if(!start)
        {
            start = true;
            trigger.TriggerDialogue();           
        }
        else if (trigger.manager.IsDialogueEnded())
        {
            Destroy(this.gameObject);
        }

    }
}
