using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(AudioListener))]

public class dialogScript : MonoBehaviour {

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

    private menuScript menu;



    void Start()
    {
        chat = GameObject.Find("Chat").GetComponent<Canvas>();
        text = GameObject.Find("Chat").GetComponentInChildren<Text>();
        dialogImage = GameObject.Find("Chat").GetComponentInChildren<Image>();

        smallInput = GameObject.FindGameObjectWithTag("Small").GetComponent<PlayerInputScript>();
        bigInput = GameObject.FindGameObjectWithTag("Big").GetComponent<PlayerInputScript>();

        menu = GameObject.Find("Menu").GetComponent<menuScript>();

        chat.enabled = false;
    }

    IEnumerator WaitForTime(float time) {
        elapsedTime = 0f;
        while (elapsedTime < time) {
            elapsedTime += Time.deltaTime;
            if (Input.GetButtonDown(smallInput.interactControl_2) || Input.GetButtonDown(bigInput.interactControl_2)) {
                yield return new WaitForSeconds(0.1f); //fixes skipping multiple lines at ones for some reason...
                break;
            }
            else yield return null;
        }
        
    }

    IEnumerator Puzzle_1(float delay) {
        if (!menu.dialoguesEnabled) yield break;
        yield return new WaitForSeconds(delay);

        chat.enabled = true;
        playedDialog_1 = true;

        //Start
        dialogImage.sprite = gimbar;
        textScrollSound.Play();
        text.text = "Ooh no! We are trapped in a game again!";
        yield return StartCoroutine(WaitForTime(3.0f + dialogueSpeed));
        //
        dialogImage.sprite = mithion;
        textScrollSound.Play();
        text.text = "Again?!";
        yield return StartCoroutine(WaitForTime(2.0f + dialogueSpeed));
        //
        dialogImage.sprite = gimbar;
        textScrollSound.Play();
        text.text = "Again!!";
        yield return StartCoroutine(WaitForTime(2.0f + dialogueSpeed));
        //
        dialogImage.sprite = mithion;
        textScrollSound.Play();
        text.text = "Which game?";
        yield return StartCoroutine(WaitForTime(2.0f + dialogueSpeed));
        //
        dialogImage.sprite = gimbar;
        textScrollSound.Play();
        text.text = "Exactly!";
        yield return StartCoroutine(WaitForTime(2.0f + dialogueSpeed));
        //
        dialogImage.sprite = mithion;
        textScrollSound.Play();
        text.text = "Whut?!";
        yield return StartCoroutine(WaitForTime(2.0f + dialogueSpeed));
        //
        dialogImage.sprite = gimbar;
        textScrollSound.Play();
        text.text = "Never mind...\n" + 
                    "Tyler, are you there...?";
        yield return StartCoroutine(WaitForTime(3.0f + dialogueSpeed));
        //
        dialogImage.sprite = gimbar;
        textScrollSound.Play();
        text.text = "......?";
        yield return StartCoroutine(WaitForTime(2.0f + dialogueSpeed));
        //
        dialogImage.sprite = mithion;
        textScrollSound.Play();
        text.text = "............?";
        yield return StartCoroutine(WaitForTime(2.0f + dialogueSpeed));
        //
        dialogImage.sprite = mithion;
        textScrollSound.Play();
        text.text = "Blast. Okay, what now?";
        yield return StartCoroutine(WaitForTime(2.5f + dialogueSpeed));
        //
        dialogImage.sprite = gimbar;
        textScrollSound.Play();
        text.text = "We should find him.\n" +
                    "But first, do as usual...";
        yield return StartCoroutine(WaitForTime(3.0f + dialogueSpeed));
        //
        dialogImage.sprite = mithion;
        textScrollSound.Play();
        text.text = "Mash all the buttons to see what abilities I have?";
        yield return StartCoroutine(WaitForTime(3.0f + dialogueSpeed));
        //
        dialogImage.sprite = gimbar;
        textScrollSound.Play();
        text.text = "Right! I should do the same.";
        yield return StartCoroutine(WaitForTime(2.0f + dialogueSpeed));
        //
        dialogImage.sprite = mithion;
        textScrollSound.Play();
        text.text = "I can spit fire, how awesome is that!?\n" +
                    "Lemme heat up that kettle over there.";
        yield return StartCoroutine(WaitForTime(4.0f + dialogueSpeed));
        //
        dialogImage.sprite = gimbar;
        textScrollSound.Play();
        text.text = "Okay, I will kick that stupid lever over here.";
        yield return StartCoroutine(WaitForTime(3.0f + dialogueSpeed));
        //End
        chat.enabled = false;
    }

    IEnumerator Puzzle_2(float delay) {
        if (!menu.dialoguesEnabled) yield break;
        yield return new WaitForSeconds(delay);

        chat.enabled = true;

        //Start
        dialogImage.sprite = gimbar;
        textScrollSound.Play();
        text.text = "Why doesn't the gate open?";
        yield return StartCoroutine(WaitForTime(2.0f + dialogueSpeed));
        //
        dialogImage.sprite = mithion;
        textScrollSound.Play();
        text.text = "I bet some mechanics have reverse functions.\n" +
                    "Let me try doing something at this kettle.";
        yield return StartCoroutine(WaitForTime(4.0f + dialogueSpeed));
        //End
        chat.enabled = false;
    }

    IEnumerator Puzzle_3(float delay) {
        if (!menu.dialoguesEnabled) yield break;
        yield return new WaitForSeconds(delay);
        chat.enabled = true;

        //Start
        dialogImage.sprite = gimbar;
        textScrollSound.Play();
        text.text = "See that ground cicrle under your feet?";
        yield return StartCoroutine(WaitForTime(3.0f + dialogueSpeed));
        //
        dialogImage.sprite = mithion;
        textScrollSound.Play();
        text.text = "Yea, why?";
        yield return StartCoroutine(WaitForTime(2.0f + dialogueSpeed));
        //
        dialogImage.sprite = gimbar;
        textScrollSound.Play();
        text.text = "It has the same color as this platform up there.\n" +
                    "See what you can do on it!";
        yield return StartCoroutine(WaitForTime(4.0f + dialogueSpeed));
        //End

        chat.enabled = false;
    }

    IEnumerator Puzzle_4(float delay) {
        if (!menu.dialoguesEnabled) yield break;
        yield return new WaitForSeconds(delay);
        chat.enabled = true;

        //Start
        dialogImage.sprite = mithion;
        textScrollSound.Play();
        text.text = "Gimbar, can you throw me over there?";
        yield return StartCoroutine(WaitForTime(3.0f + dialogueSpeed));
        //End

        chat.enabled = false;
    }

    IEnumerator Puzzle_5(float delay) {
        if (!menu.dialoguesEnabled) yield break;
        yield return new WaitForSeconds(delay);
        chat.enabled = true;

        //Start
        dialogImage.sprite = gimbar;
        textScrollSound.Play();
        text.text = "Ah nice, a soccerfield!";
        yield return StartCoroutine(WaitForTime(2.0f + dialogueSpeed));
        //
        dialogImage.sprite = mithion;
        textScrollSound.Play();
        text.text = "It's called football,\n" +
                    "Y U No Learn Dis?!";
        yield return StartCoroutine(WaitForTime(3.0f + dialogueSpeed));
        //End

        chat.enabled = false;
    }

    IEnumerator Puzzle_6(float delay) {
        if (!menu.dialoguesEnabled) yield break;
        yield return new WaitForSeconds(delay);
        chat.enabled = true;

        //Start
        dialogImage.sprite = gimbar;
        textScrollSound.Play();
        text.text = "Holy %!&$,\n" +
                    "the devs are trying to kill us! :|";
        yield return StartCoroutine(WaitForTime(3.0f + dialogueSpeed));
        //End

        chat.enabled = false;
    }

    IEnumerator Puzzle_7(float delay) {
        if (!menu.dialoguesEnabled) yield break;
        yield return new WaitForSeconds(delay);
        chat.enabled = true;

        //Start
        dialogImage.sprite = gimbar;
        textScrollSound.Play();
        text.text = "010101";
        yield return StartCoroutine(WaitForTime(3.0f + dialogueSpeed));
        //
        dialogImage.sprite = mithion;
        textScrollSound.Play();
        text.text = "What the %&§$. What are you saying?";
        yield return StartCoroutine(WaitForTime(3.0f + dialogueSpeed));
        //
        dialogImage.sprite = gimbar;
        textScrollSound.Play();
        text.text = "I don't know...\n" +
                    "Maybe it's a hint from the developers.";
        yield return StartCoroutine(WaitForTime(3.0f + dialogueSpeed));
        //
        dialogImage.sprite = mithion;
        textScrollSound.Play();
        text.text = "Couldn't they let you say something useful?";
        yield return StartCoroutine(WaitForTime(3.0f + dialogueSpeed));
        //End

        chat.enabled = false;
    }

    IEnumerator Puzzle_8(float delay) {
        if (!menu.dialoguesEnabled) yield break;
        yield return new WaitForSeconds(delay);
        chat.enabled = true;

        //Start
        dialogImage.sprite = gimbar;
        textScrollSound.Play();
        text.text = "Wow, thats dark. Can you lit that torch?\n" +
                    "I will take it with me.";
        yield return StartCoroutine(WaitForTime(3.0f + dialogueSpeed));
        //End

        chat.enabled = false;
    }

    IEnumerator Puzzle_9(float delay) {
        if (!menu.dialoguesEnabled) yield break;
        yield return new WaitForSeconds(delay);
        chat.enabled = true;

        //Start
        dialogImage.sprite = mithion;
        textScrollSound.Play();
        text.text = "Oh no... I hate those kind of puzzles!";
        yield return StartCoroutine(WaitForTime(3.0f + dialogueSpeed));
        //
        dialogImage.sprite = gimbar;
        textScrollSound.Play();
        text.text = "Stop whining and help me find the\n" + 
                    "right sequence.";
        yield return StartCoroutine(WaitForTime(3.0f + dialogueSpeed));
        //End

        chat.enabled = false;
    }

    IEnumerator Puzzle_10(float delay) {
        if (!menu.dialoguesEnabled) yield break;
        yield return new WaitForSeconds(delay);
        chat.enabled = true;

        //Start
        dialogImage.sprite = mithion;
        textScrollSound.Play();
        text.text = "I found Tyler!";
        yield return StartCoroutine(WaitForTime(2.0f + dialogueSpeed));
        //
        dialogImage.sprite = tyler;
        textScrollSound.Play();
        text.text = "Hey Guys! Please, get me out of here!";
        yield return StartCoroutine(WaitForTime(3.0f + dialogueSpeed));
        //
        dialogImage.sprite = gimbar;
        textScrollSound.Play();
        text.text = "Just this last puzzle, brother.";
        yield return StartCoroutine(WaitForTime(2.0f + dialogueSpeed));
        //End

        chat.enabled = false;
    }

    IEnumerator Puzzle_11(float delay) {
        if (!menu.dialoguesEnabled) yield break;
        yield return new WaitForSeconds(delay);
        chat.enabled = true;

        //Start
        dialogImage.sprite = tyler;
        textScrollSound.Play();
        text.text = "Thanks for saving me guys !\nYou are real friends !!";
        yield return StartCoroutine(WaitForTime(3.0f + dialogueSpeed));
        //
        dialogImage.sprite = mithion;
        textScrollSound.Play();
        text.text = "No problem !";
        yield return StartCoroutine(WaitForTime(2.0f + dialogueSpeed));
        //
        dialogImage.sprite = gimbar;
        textScrollSound.Play();
        text.text = "What do we do now???";
        yield return StartCoroutine(WaitForTime(2.0f + dialogueSpeed));
        //
        dialogImage.sprite = mithion;
        textScrollSound.Play();
        text.text = "Well, I don't know.. ALT - F4? Like usual..?";
        yield return StartCoroutine(WaitForTime(5.0f + dialogueSpeed));
        //End

        chat.enabled = false;
    }

}