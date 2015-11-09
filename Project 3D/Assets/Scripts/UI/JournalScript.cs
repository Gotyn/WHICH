using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class JournalScript : MonoBehaviour {

    //List<string> journalEntries = new List<string>();
    int journalEntryCount = -1;
    GameObject journal;
    GameObject currentEntry, previousEntry;
    Text entryText;

    RectTransform rect;
    bool active = false;

    float entryOffset = 35;
    bool shouldBeActiveAfterPause = false;
    [SerializeField]
    AudioSource notificationSound;

    [SerializeField]
    Image notificationText;
    MenuScript menuScript;
    void Start() {
        journal = GameObject.Find("Journal");
        journal.SetActive(active);
        notificationText.enabled = false;
        menuScript = MenuScript.Instance;
    }

    void Update() {
        if ((Input.GetButtonDown("L3") || Input.GetButtonDown("R3")) && !menuScript.paused) {
            active = !active;
            shouldBeActiveAfterPause = active;
            journal.SetActive(active);
        }
        if (menuScript.paused) {
            journal.SetActive(false);
        }
        else if (shouldBeActiveAfterPause) {
            journal.SetActive(true);
        }

    }

    public void AddJournalEntry(string text) {
        if (currentEntry != null) { previousEntry = currentEntry; }
        journalEntryCount++;
        
        //Add a new gameobject with a textcomponent
        currentEntry = new GameObject("Entry_" + journalEntryCount);
        currentEntry.AddComponent<Text>();
        
        SetJournalAsParent(currentEntry); //Set the text as child of journal with the same position and scale
        FixRectSettings(currentEntry, 850, 50);

        entryText = currentEntry.GetComponent<Text>();
        FixTextSettings(entryText, Resources.Load<Font>("Fonts/blackchancery"));

        FixCorrectPosition();

        //Set the actual text
        entryText.text = text;

        if (previousEntry != null) GreyOutEntry();

        notificationSound.Play();
        notificationText.enabled = true;
        Invoke("DisableNotificationText", 2f);
    }

    void GreyOutEntry() {
        previousEntry.GetComponent<Text>().color = new Color(1, 1, 1, 0.2f);
    }

    void DisableNotificationText() {
        notificationText.enabled = false;
    }

    void FixTextSettings(Text text, Font font) {
        //Get a reference to the TextScript and set 'Character' and 'Paragraph' settings
        text.font = font;
        text.fontSize = 30;
        text.alignment = TextAnchor.MiddleCenter;
        text.horizontalOverflow = HorizontalWrapMode.Wrap;
        text.verticalOverflow = VerticalWrapMode.Overflow;
    }

    void FixRectSettings(GameObject obj, float width, float height) {
        //Gain acces to the RectTransform and set the appropiate size
        rect = obj.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0, 0);
        rect.anchorMax = new Vector2(1, 1);

    }

    //Sets the Journal as parent and gives the child the same position
    void SetJournalAsParent(GameObject child) {
        child.transform.SetParent(journal.transform);
        child.transform.position = journal.transform.position + new Vector3(0, Screen.height/3.5f, 0);
        child.transform.localScale = new Vector3(1, 1, 1);
    }

    void FixCorrectPosition() {
        entryOffset = Screen.height / 15;
        currentEntry.transform.position -= new Vector3(0, (entryOffset*journalEntryCount), 0);
    }
}
