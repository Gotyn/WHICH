using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CheckpointScript : MonoBehaviour {

	//Bros
    GameObject small;
    GameObject big;

	//SpawnPoints
	public GameObject newSpawnPointSmall;
	public GameObject newSpawnPointBig;

	//Waiting for players/dialog
	Image waitingForPlayerText, waitingForDialogueText;

	//CeckPoint objects/values
	public GameObject invWall;
	bool used = false;
	bool smallEntered = false;
	bool bigEntered = false;

	//Dialog
	DialogueScript dialogue; 
	public int playDialogue;
	public float delayDialogue = 2f;

	//Doors
    public List<GameObject> doorsToClose = new List<GameObject>();
 
	//ToolTips
    [Tooltip("Normal-ROTATION- 45, 270, 0\nSoccerfield-ROTATION- 45, 0, 0\nDARK ROOM-ROTATION- 45, 180, 0")]
    public Quaternion newRotation;
    [Tooltip("Normal-OFFSET- 20, 20, 0\nSoccerfield-OFFSET- 0, 20, -20\nDARK ROOM-OFFSET- 0, 20, 20")]
    public Vector3 newOffset;
    [Tooltip("Zoom out value only when the distance between player is bigger !!")]

	//new Camera Values
	BetterCameraScript betterCamScript;
    public float ZoomOutValue;
    Quaternion oldRot;
    Quaternion cameraRotation;
  

	// Use this for initialization
	void Start () {
        //bros
        small = GameManagerScript.SB;
        big = GameManagerScript.BB;

        //waiting for player text
        waitingForPlayerText = GameObject.Find("WaitingForPlayerText").GetComponent<Image>();
        waitingForDialogueText = GameObject.Find("WaitingForDialogueText").GetComponent<Image>();

		//dialog
		dialogue = FindObjectOfType<GameManagerScript>().GetComponent<DialogueScript>();

        betterCamScript = Camera.main.GetComponent<BetterCameraScript>();
    }

	void Update () {
		WaitForCheck ();
    }

	void WaitForCheck() {
		//If both players entered and the dialogue is not playing
		if (smallEntered && bigEntered && !dialogue.chat.enabled) {
			invWall.SetActive (false); 
			if (!used) MoveToNext (); 
			waitingForPlayerText.enabled = false;
			waitingForDialogueText.enabled = false;
		}
		//if both players entered but the dialogue is still playing
		else if (smallEntered && bigEntered && !used) {
			waitingForPlayerText.enabled = false;
			waitingForDialogueText.enabled = true;
		}
		//if one of the players entered and dialogue is still playing
		else if ((smallEntered || bigEntered) && !used) {
			waitingForPlayerText.enabled = true;
		}
	}

	//Plays new dialog, changes camera values and closes doors
	void MoveToNext (){

		small.GetComponent<CameraControlScript> ().spawn = newSpawnPointSmall;
		big.GetComponent<CameraControlScript> ().spawn = newSpawnPointBig;

        //----------------------------------------------
		ChangeCameraValues ();
        //----------------------------------------------
		CloseDoors ();
      
		//play next dialogue
        dialogue.StartCoroutine("Puzzle_" + playDialogue.ToString(), 2f);

		used = true;

	}
	void ChangeCameraValues () {
		betterCamScript.offset = newOffset;
		betterCamScript.newRotation = Quaternion.Euler(newRotation.x, newRotation.y, newRotation.z);
		betterCamScript.startTime = Time.time;
		betterCamScript.length = 0.6f;
		betterCamScript.myZoomValue = ZoomOutValue;
	}

	void CloseDoors () {
		if (doorsToClose.Count > 0)
		{
			for (int i = 0; i < doorsToClose.Count; i++)
			{
				if (doorsToClose[i].GetComponent("ExpertDoorScript") as ExpertDoorScript != null)
				{
					doorsToClose[i].GetComponent<ExpertDoorScript>().dirtyOpen = false;
					doorsToClose[i].GetComponent<ExpertDoorScript>().completed.Clear();
				}
				doorsToClose[i].GetComponent<DoorScript>().dirtyOpen = false;
				doorsToClose[i].GetComponent<DoorScript>().completed.Clear();
			}
		}
	}

	void Disable () {
		this.gameObject.SetActive (false);
	}
	
    void OnTriggerEnter(Collider hit)
    {
		if (hit.transform.CompareTag ("Small")) {
			smallEntered = true;
		}

		if (hit.transform.CompareTag ("Big")) {
			bigEntered = true;
		}
    }

	void OnTriggerExit(Collider hit)
	{
		if (hit.transform.CompareTag ("Small")) {
			smallEntered = false;
			waitingForPlayerText.enabled = false;
            waitingForDialogueText.enabled = false;
		}
		
		if (hit.transform.CompareTag ("Big")) {
			bigEntered = false;
			waitingForPlayerText.enabled = false;
            waitingForDialogueText.enabled = false;
        }
	}
}
