using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class dialogScript : MonoBehaviour {

    Text text;
    Canvas chat;
    public Sprite gimbar, mithion, tyler;
    Image asd;

    float dialogueSpeed = 2f;

    public bool playedDialog_1 = false;

    // Use this for initialization

    void Start()
    {
        chat = GameObject.Find("Chat").GetComponent<Canvas>();
        text = GameObject.Find("Chat").GetComponentInChildren<Text>();
        asd = GameObject.Find("Chat").GetComponentInChildren<Image>();

        chat.enabled = false;


    }


    IEnumerator Puzzle_1()
    {
        chat.enabled = true;

        asd.sprite = mithion;
        text.text = "Ooh no! We are trapped in a game again!";
        yield return new WaitForSeconds(dialogueSpeed);
        asd.sprite = gimbar;
        text.text = "Again?!";
        yield return new WaitForSeconds(dialogueSpeed);
        asd.sprite = mithion;
        text.text = "Again!!";
        yield return new WaitForSeconds(dialogueSpeed);
        chat.enabled = false;


        playedDialog_1 = true;
    }

    IEnumerator Puzzle_2()
    {
        chat.enabled = true;

        asd.sprite = mithion;
        text.text = "Asd";
        yield return new WaitForSeconds(dialogueSpeed);
        chat.enabled = false;

    }
    /*   1st Puzzle(Start):
   G: "Ooh no! We are trapped in a game again!"

   M: "Again?!" 

   G: "Again!!"

   M: "Which game?"

   G: "Exactly!"

   M: "What?!"
   G: "Never mind. Tyler, are you there?"
   *silence*
   M: "Okay, what know?". 
   G: "First do as usual". 
   M: "Mash all the buttons to see what abilities I have?". 
   G: "Right! I should do the same". 
   *buttonmashing to test the controlls*
   M: "I can spit fire, how awesome is that!? Let me fire up this fireplate". 
   G: "Okay, I will kick that stupid lever over there".
           */
}