using UnityEngine;
using System.Collections;

public class DialogueTrigger : MonoBehaviour
{



    DialogueScript dialogue;
    public int playDialogue;
    public float delayDialogue = 2f;

    bool played = false;


    // Use this for initialization
    void Start()
    {
        dialogue = FindObjectOfType<DialogueScript>();
    }

    void OnTriggerEnter(Collider hit)
    {
        if (hit.CompareTag("Small") || hit.CompareTag("Big"))
        {
            if (!played)
            {
                played = true;
                dialogue.StartCoroutine("Puzzle_" + playDialogue.ToString(), 2f);                   
            }
        }
    }
}
