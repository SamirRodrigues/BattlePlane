using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogText;
    public Image avatarSprite;
    public GameObject dialogPlane;

    private bool dialogueEnd = false;


    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        dialogueEnd = false;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogPlane.SetActive(true);

        nameText.text = dialogue.name;
        avatarSprite.sprite = dialogue.avatar;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisolayNextSentence();
    }

    public void DisolayNextSentence()
    {
        if(sentences.Count == 0) //end queue
        {
            EndDialogue();
            return;
        }

        dialogText.text = sentences.Dequeue();
    }

    public void EndDialogue()
    {
        dialogueEnd = true;
        dialogPlane.SetActive(false);
    }

    public bool IsDialogueEnded()
    {
        return dialogueEnd;
    }
}
