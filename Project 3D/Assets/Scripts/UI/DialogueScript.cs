﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(AudioListener))]

public class DialogueScript : MonoBehaviour {
    Text text;
    [HideInInspector]
    public Canvas chat;
    public Sprite gimbar, mithion, tyler;
    Image dialogImage;



    public float dialogueSpeed = 0f;

    [HideInInspector]
    public bool playedDialog_1 = false;

    public AudioSource textScrollSound;

    private float elapsedTime;

    private PlayerInputScript smallInput;
    private PlayerInputScript bigInput;
   
    private MenuScript menu;
    private JournalScript journalScript;

    void Start()
    {
        menu = MenuScript.Instance;
        chat = GameObject.Find("Chat").GetComponent<Canvas>();
        text =  chat.GetComponentInChildren<Text>();
        dialogImage = chat.GetComponentInChildren<Image>();
    
        journalScript = GetComponent<JournalScript>();
        smallInput = GameManagerScript.SB.GetComponent<PlayerInputScript>();
        bigInput = GameManagerScript.BB.GetComponent<PlayerInputScript>();
        
        chat.enabled = false;
    }

    void Gimbar(string dialogue) {
        dialogImage.sprite = gimbar;
        textScrollSound.Play();
        text.text = dialogue;
    }

    void Mithion(string dialogue) {
        dialogImage.sprite = mithion;
        textScrollSound.Play();
        text.text = dialogue;
    }

    void Tyler(string dialogue) {
        dialogImage.sprite = tyler;
        textScrollSound.Play();
        text.text = dialogue;
    }
   
    IEnumerator WaitForTime(float time) {
        elapsedTime = 0f;
        while (elapsedTime < time) {
            elapsedTime += Time.deltaTime;
            if ((Input.GetButtonDown(smallInput.interactControl_2) || Input.GetButtonDown(bigInput.interactControl_2) || DPadButtons.right)) {

                yield return new WaitForSeconds(0.2f);
                break;
            }
            else yield return null;
        }
    }

    #region "Puzzles"
    IEnumerator Puzzle_0(float delay)
    {
        yield return null;
    }

    IEnumerator Puzzle_1(float delay)
    {
        playedDialog_1 = true;
        if (!menu.dialoguesEnabled) yield break;
        yield return new WaitForSeconds(delay);

        chat.enabled = true;
  
        //Start
        Gimbar("Ooh no! We are trapped in a game again!");
        yield return StartCoroutine(WaitForTime(3.0f + dialogueSpeed));
        //
        Mithion("Again?!");
        yield return StartCoroutine(WaitForTime(2.0f + dialogueSpeed));
        //
        Gimbar("Again!!");
        yield return StartCoroutine(WaitForTime(2.0f + dialogueSpeed));
        //
        Mithion("Which game?");
        yield return StartCoroutine(WaitForTime(2.0f + dialogueSpeed));
        //
        Gimbar("Exactly!");
        yield return StartCoroutine(WaitForTime(2.0f + dialogueSpeed));
        //
        Mithion("Whut?");
        yield return StartCoroutine(WaitForTime(2.0f + dialogueSpeed));
        //
        Gimbar("Never mind...\n" +
               "Tyler, are you there...?");
        yield return StartCoroutine(WaitForTime(3.0f + dialogueSpeed));
        //
        Gimbar(".....?");
        yield return StartCoroutine(WaitForTime(2.0f + dialogueSpeed));
        //
        Mithion("............?");
        yield return StartCoroutine(WaitForTime(2.0f + dialogueSpeed));
        //
        Mithion("Blast. Okay, what now?");
        yield return StartCoroutine(WaitForTime(2.5f + dialogueSpeed));
        //
        Gimbar("We should find him.\n" +
               "But first, do as usual...");
        yield return StartCoroutine(WaitForTime(3.0f + dialogueSpeed));
        //
        Mithion("Mash all the buttons to see what abilities I have?");
        yield return StartCoroutine(WaitForTime(3.0f + dialogueSpeed));
        //
        Gimbar("Right! I should do the same.");
        yield return StartCoroutine(WaitForTime(2.0f + dialogueSpeed));
        //
        Mithion("I can spit fire, how awesome is that!?\n" +
                "Lemme heat up that kettle over there.");
        yield return StartCoroutine(WaitForTime(4.0f + dialogueSpeed));
        //
        Gimbar("Okay, I will kick that stupid lever over here.");
        yield return StartCoroutine(WaitForTime(3.0f + dialogueSpeed));
        //End
        journalScript.AddJournalEntry("* We lost Tyler. We must find him!");
        //journalScript.GiveNotification();

        chat.enabled = false;
    }

    IEnumerator Puzzle_2(float delay) {
        if (!menu.dialoguesEnabled) yield break;
        yield return new WaitForSeconds(delay);

        chat.enabled = true;

        //Start
        Gimbar("Why doesn't the gate open");
        yield return StartCoroutine(WaitForTime(2.0f + dialogueSpeed));
        //
        Mithion("I bet some mechanics have reverse functions.\n" +
                "Let me try doing something at this kettle.");
        yield return StartCoroutine(WaitForTime(4.0f + dialogueSpeed));
        //End

        chat.enabled = false;
    }

    IEnumerator Puzzle_3(float delay) {
        if (!menu.dialoguesEnabled) yield break;
        yield return new WaitForSeconds(delay);
        chat.enabled = true;

        //Start
        Gimbar("Do you see that cicrle under your feet?");
        yield return StartCoroutine(WaitForTime(3.0f + dialogueSpeed));
        //
        Mithion("Yeah, what about it?");
        yield return StartCoroutine(WaitForTime(2.0f + dialogueSpeed));
        //
        Gimbar("It has the same color as this platform up there.\n" +
               "See what you can do on it!");
        yield return StartCoroutine(WaitForTime(4.0f + dialogueSpeed));
        //End

        chat.enabled = false;
    }

    IEnumerator Puzzle_4(float delay) {
        if (!menu.dialoguesEnabled) yield break;
        yield return new WaitForSeconds(delay);
        chat.enabled = true;

        //Start
        Mithion("Gimbar, could you throw me over there?");
        yield return StartCoroutine(WaitForTime(3.0f + dialogueSpeed));
        //End

        chat.enabled = false;
    }

    IEnumerator Puzzle_5(float delay) {
        if (!menu.dialoguesEnabled) yield break;
        yield return new WaitForSeconds(4);
        chat.enabled = true;

        //Start
        Gimbar("Ah, nice! A soccerfield!");
        yield return StartCoroutine(WaitForTime(2.0f + dialogueSpeed));
        //
        Mithion("It's called football,\n" +
                "Y U No Learn Dis?!");
        yield return StartCoroutine(WaitForTime(3.0f + dialogueSpeed));
        //
        Mithion("These goals seem to be some kind of trigger...\n" +
                "Let's play and see what happens.");
        //End
        
        journalScript.AddJournalEntry("* These goals seem to be some kind of trigger.");
        chat.enabled = false;
    }

    IEnumerator Puzzle_6(float delay) {
        if (!menu.dialoguesEnabled) yield break;
        yield return new WaitForSeconds(delay);
        chat.enabled = true;

        //Start
        Gimbar("Holy %!&$,\n" +
               "the devs are trying to kill us! :|");
        yield return StartCoroutine(WaitForTime(3.0f + dialogueSpeed));
        //End

        chat.enabled = false;
    }

    IEnumerator Puzzle_7(float delay) {
        if (!menu.dialoguesEnabled) yield break;
        yield return new WaitForSeconds(delay);
        chat.enabled = true;

        //Start
        Gimbar("010101");
        yield return StartCoroutine(WaitForTime(3.0f + dialogueSpeed));
        //
        Mithion("What the %&§$. What are you saying?");
        yield return StartCoroutine(WaitForTime(3.0f + dialogueSpeed));
        //
        Gimbar("I don't know...\n" +
               "Maybe it's a hint from the developers.");
        yield return StartCoroutine(WaitForTime(3.0f + dialogueSpeed));
        //
        Mithion("Couldn't they let you say something useful?");
        yield return StartCoroutine(WaitForTime(3.0f + dialogueSpeed));
        //End

        journalScript.AddJournalEntry("* 010101 - A hint from the devs.");
        chat.enabled = false;
    }

    IEnumerator Puzzle_8(float delay) {
        if (!menu.dialoguesEnabled) yield break;
        yield return new WaitForSeconds(delay);
        chat.enabled = true;

        //Start
        Gimbar("Wow, thats dark. Can you lit that campfire?\n" +
               "We might be able to see something then...");
        yield return StartCoroutine(WaitForTime(3.0f + dialogueSpeed));
        //End

        journalScript.AddJournalEntry("* We fell into a dark cave. Where is the exit?");

        chat.enabled = false;
    }

    IEnumerator Puzzle_81(float delay) {
        if (!menu.dialoguesEnabled) yield break;
        yield return new WaitForSeconds(delay);
        chat.enabled = true;

        //Start
        Mithion("Hey, a torch! Let's divide the work.\n" +
                "I'll light her up, you carry her around.");
        yield return StartCoroutine(WaitForTime(3.0f + dialogueSpeed));
        //End

        journalScript.AddJournalEntry("* Holding a lit torch might come in handy in such a dark room.");
        chat.enabled = false;
    }

    IEnumerator Puzzle_82(float delay) {
        if (!menu.dialoguesEnabled) yield break;
        yield return new WaitForSeconds(delay);
        chat.enabled = true;

        //Start
        Mithion("Great teamwork, Gimbar.");
        yield return StartCoroutine(WaitForTime(2.0f + dialogueSpeed));
        Gimbar("Yep.");
        yield return StartCoroutine(WaitForTime(1.0f + dialogueSpeed));
        //End

        journalScript.AddJournalEntry("* Apparently, teamwork is important somehow.");
        chat.enabled = false;
    }

    IEnumerator Puzzle_83(float delay) {
        if (!menu.dialoguesEnabled) yield break;
        yield return new WaitForSeconds(delay);
        chat.enabled = true;

        //Start
        Gimbar("This line looks similar to the soccerfield lines...");
        yield return StartCoroutine(WaitForTime(2.0f + dialogueSpeed));
        Mithion("Cool, I bet some balls are involved then.");
        yield return StartCoroutine(WaitForTime(2.0f + dialogueSpeed));
        //End

        journalScript.AddJournalEntry("* The flagpole reminds me of golf.");
        chat.enabled = false;
    }

    IEnumerator Puzzle_84(float delay) {
        if (!menu.dialoguesEnabled) yield break;
        yield return new WaitForSeconds(delay);
        chat.enabled = true;

        //Start
        Mithion("Is that the exit up ahead?");
        yield return StartCoroutine(WaitForTime(2.0f + dialogueSpeed));
        Gimbar("I sure hope so.. My eyes are getting pretty tired.");
        yield return StartCoroutine(WaitForTime(2.0f + dialogueSpeed));
        //End

        journalScript.AddJournalEntry("* We can almost taste the daylight.");

        chat.enabled = false;
    }

    IEnumerator Puzzle_9(float delay) {
        if (!menu.dialoguesEnabled) yield break;
        yield return new WaitForSeconds(4);
        chat.enabled = true;

        //Start
        Mithion("Oh no... I hate those kind of puzzles!");
        yield return StartCoroutine(WaitForTime(3.0f + dialogueSpeed));
        //
        Gimbar("Stop whining and help me find the\n" +
               "right combination.");
        yield return StartCoroutine(WaitForTime(3.0f + dialogueSpeed));
        //End

        journalScript.AddJournalEntry("* Work together to find the combination.");
        chat.enabled = false;
    }

    IEnumerator Puzzle_10(float delay) {
        if (!menu.dialoguesEnabled) yield break;
        yield return new WaitForSeconds(delay);
        chat.enabled = true;

        //Start
        Mithion("We found Tyler!");
        yield return StartCoroutine(WaitForTime(2.0f + dialogueSpeed));
        //
        Tyler("Hey Guys! Please, get me out of here!");
        yield return StartCoroutine(WaitForTime(3.0f + dialogueSpeed));
        //
        Gimbar("Just this last puzzle, brother.");
        yield return StartCoroutine(WaitForTime(2.0f + dialogueSpeed));
        //End

        journalScript.AddJournalEntry("* We found Tyler. But he's trapped.");
        chat.enabled = false;
    }

    IEnumerator Puzzle_11(float delay) {
        if (!menu.dialoguesEnabled) yield break;
        yield return new WaitForSeconds(delay);
        chat.enabled = true;

        //Start
        Tyler("Thanks for saving me guys!\n" + 
              "You are real friends!");
        yield return StartCoroutine(WaitForTime(3.0f + dialogueSpeed));
        //
        Mithion("No problem!");
        yield return StartCoroutine(WaitForTime(2.0f + dialogueSpeed));
        //
        Gimbar("What do we do now?");
        yield return StartCoroutine(WaitForTime(2.0f + dialogueSpeed));
        //
        Mithion("I think we're done here.");
        yield return StartCoroutine(WaitForTime(3.0f + dialogueSpeed));
        //
        Tyler("Ok. Bye.");
        yield return StartCoroutine(WaitForTime(5.0f + dialogueSpeed));
        //End

        journalScript.AddJournalEntry("* We saved Tyler!");

        chat.enabled = false;

        GameManagerScript.gameCompleted = true;
    }

	IEnumerator Puzzle_100(float delay) {
		if (!menu.dialoguesEnabled) yield break;

		while (chat.enabled) { 
			yield return new WaitForSeconds(0.1f); 
		}

		yield return new WaitForSeconds(delay);
		chat.enabled = true;
		
		//Start
		Mithion("Hey Gimbar, something lit up over there.\n" + 
		        "Can you go and check it out?");
		yield return StartCoroutine(WaitForTime(3.0f + dialogueSpeed));
		//End
		
		chat.enabled = false;
	}


    #endregion "Puzzles"
}