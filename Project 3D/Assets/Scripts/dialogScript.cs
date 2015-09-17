using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class dialogScript : MonoBehaviour {

    Text text;
    [HideInInspector]
    public Canvas chat;
    public Sprite gimbar, mithion, tyler;
    Image asd;

    float dialogueSpeed = 2f;

    [HideInInspector]
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
        playedDialog_1 = true;
        asd.sprite = gimbar;
        text.text = "Ooh no! We are trapped in a game again!";
        yield return new WaitForSeconds(dialogueSpeed);
        asd.sprite = mithion;
        text.text = "Again?!";
        yield return new WaitForSeconds(dialogueSpeed);

        asd.sprite = gimbar;
        text.text = "Again!!";
        yield return new WaitForSeconds(dialogueSpeed);
        asd.sprite = mithion;
        text.text = "Which game?";
        yield return new WaitForSeconds(dialogueSpeed);

        asd.sprite = gimbar;
        text.text = "Exactly!";
        yield return new WaitForSeconds(dialogueSpeed);
        asd.sprite = mithion;
        text.text = "What?!";
        yield return new WaitForSeconds(dialogueSpeed);

        asd.sprite = gimbar;
        text.text = "Never mind. Tyler, are you there?";
        yield return new WaitForSeconds(dialogueSpeed);
        asd.sprite = mithion;
        text.text = "Okay, what now?";
        yield return new WaitForSeconds(dialogueSpeed);

        asd.sprite = gimbar;
        text.text = "First do as usual";
        yield return new WaitForSeconds(dialogueSpeed);
        asd.sprite = mithion;
        text.text = "Mash all the buttons to see what abilities I have?";
        yield return new WaitForSeconds(dialogueSpeed);

        asd.sprite = gimbar;
        text.text = "Right! I should do the same";
        yield return new WaitForSeconds(dialogueSpeed);
        chat.enabled = false;
        asd.sprite = mithion;
        text.text = "I can spit fire, how awesome is that!? Let me fire up this fireplate";
        yield return new WaitForSeconds(dialogueSpeed);

        asd.sprite = gimbar;
        text.text = "Okay, I will kick that stupid lever over there";
        yield return new WaitForSeconds(dialogueSpeed);
        chat.enabled = false;

    }

    IEnumerator Puzzle_2()
    {
        chat.enabled = true;

        asd.sprite = gimbar;
        text.text = "Why doesn't the gate open?";
        yield return new WaitForSeconds(dialogueSpeed);
        asd.sprite = mithion;
        text.text = "I bet some mechanics have reverse functions. Let me try doing something at this firepot";
        yield return new WaitForSeconds(dialogueSpeed);
        chat.enabled = false;

    }
    
    IEnumerator Puzzle_3()
    {
        chat.enabled = true;

        asd.sprite = gimbar;
        text.text = "See that ground cicrle under your feet?";
        yield return new WaitForSeconds(dialogueSpeed);
        asd.sprite = mithion;
        text.text = "Yea, why?";
        yield return new WaitForSeconds(dialogueSpeed);
        asd.sprite = gimbar;
        text.text = "It has the same color as this platform up there. See what you can do on it!";
        yield return new WaitForSeconds(dialogueSpeed);
        chat.enabled = false;
    }

    IEnumerator Puzzle_4()
    {
        chat.enabled = true;

        asd.sprite = mithion;
        text.text = "Gimbar, can you throw me over there?";
        yield return new WaitForSeconds(dialogueSpeed);
        chat.enabled = false;

    }

    IEnumerator Puzzle_5()
    {
        chat.enabled = true;

        asd.sprite = gimbar;
        text.text = "SAh nice, a soccerfield!";
        yield return new WaitForSeconds(dialogueSpeed);
        asd.sprite = mithion;
        text.text = "It's called football, Y U NO LEARN DIS?!";
        yield return new WaitForSeconds(dialogueSpeed);
        chat.enabled = false;
    }

    IEnumerator Puzzle_6()
    {
        chat.enabled = true;

        asd.sprite = gimbar;
        text.text = "HOLY §&%$, the devs are trying to kill us!";
        yield return new WaitForSeconds(dialogueSpeed);
        chat.enabled = false;

    }

    IEnumerator Puzzle_7()
    {
        chat.enabled = true;

        asd.sprite = gimbar;
        text.text = "010101";
        yield return new WaitForSeconds(dialogueSpeed);
        asd.sprite = mithion;
        text.text = "What the %&§$. What are you saying?";
        yield return new WaitForSeconds(dialogueSpeed);

        asd.sprite = gimbar;
        text.text = "I don't know, maybe it's a hint from the developers";
        yield return new WaitForSeconds(dialogueSpeed);
        asd.sprite = mithion;
        text.text = "Couldn't they let you say something useful?";
        yield return new WaitForSeconds(dialogueSpeed);
        chat.enabled = false;
    }

    IEnumerator Puzzle_8()
    {
        chat.enabled = true;

        asd.sprite = gimbar;
        text.text = "Wow, thats dark. Can you lit that torch? I will take it with me";
        yield return new WaitForSeconds(dialogueSpeed);
        chat.enabled = false;

    }

    IEnumerator Puzzle_9()
    {
        chat.enabled = true;

        asd.sprite = mithion;
        text.text = "Oh no, I hate those kind of puzzles!";
        yield return new WaitForSeconds(dialogueSpeed);
        asd.sprite = gimbar;
        text.text = "Stop whining, lets find the right sequence";
        yield return new WaitForSeconds(dialogueSpeed);
        chat.enabled = false;
    }

    IEnumerator Puzzle_10()
    {
        chat.enabled = true;

        asd.sprite = mithion;
        text.text = "I found Tyler!";
        yield return new WaitForSeconds(dialogueSpeed);
        asd.sprite = tyler;
        text.text = "Hey Guys! Get me out of here!";
        yield return new WaitForSeconds(dialogueSpeed);
        asd.sprite = gimbar;
        text.text = "Just this last puzzle, brother";
        yield return new WaitForSeconds(dialogueSpeed);
        chat.enabled = false;

    }

    /*
       1st Puzzle(Start):
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