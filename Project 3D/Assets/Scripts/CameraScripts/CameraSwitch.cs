using UnityEngine;
using System.Collections;

public class CameraSwitch : MonoBehaviour {

	public Camera cameraMain;
	public Camera cameraCutScene;

    MenuScript menu;

	bool isPlaying = false;
	bool isInGame = false;
    

    DialogueScript dialog;
	PlayerMovement sBroMovement;
	PlayerMovement bBroMovement;

	// Use this for initialization
	void Start () {
        dialog = GameObject.Find("GameManager").GetComponent<DialogueScript>();
        sBroMovement = GameManagerScript.SB.GetComponent<PlayerMovement> ();
		bBroMovement = GameManagerScript.BB.GetComponent<PlayerMovement> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isPlaying && !isInGame && (Input.GetButtonDown ("SMALL_INTERACT_2") || Input.GetButtonDown ("BIG_INTERACT_2") || DPadButtons.right)) {
            
            Skip();
		}

    }

	public void Play () {
        if (Application.loadedLevel == 0) InvincibleScript.Instance.currentPosition = SetPositionOfPlayers.Positions.FirstSceneStart;
        sBroMovement.enabled = false;
		sBroMovement.GetComponentInChildren<SBGrounded> ().cutScene = true;
        sBroMovement.GetComponentInChildren<FireAttackScript>().enabled = false;

        bBroMovement.enabled = false;
        bBroMovement.GetComponentInChildren<BBGrounded>().cutScene = true;


        cameraCutScene.enabled = true;
        cameraCutScene.GetComponent<Animation>().Play();
        isPlaying = true;

		Invoke("SwitchToMain", 21.1f);
	}

	void Skip () {
		isInGame = true;
        cameraCutScene.GetComponent<Animation>().Stop();
		SwitchToMain();
	}

	void SwitchToMain () {
        cameraCutScene.enabled = false;
		cameraMain.enabled = true;
        if (!dialog.playedDialog_1) dialog.StartCoroutine("Puzzle_1", 2.0f);
		bBroMovement.GetComponent<PlayerMovement> ().enabled = true;
		sBroMovement.GetComponentInChildren<SBGrounded> ().cutScene = false;
		sBroMovement.GetComponent<PlayerMovement> ().enabled = true;
        bBroMovement.GetComponentInChildren<BBGrounded>().cutScene = false;
        sBroMovement.GetComponentInChildren<FireAttackScript> ().enabled = true;
	}

    //This is for switching to Normal camera when coming back to the first scene
    public void SwitchToNormal() {
        if (Application.loadedLevel == 0) {
            cameraCutScene.enabled = false;
        }
        cameraMain.enabled = true;
    }
}
