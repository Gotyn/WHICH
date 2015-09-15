using UnityEngine;
using System.Collections;

public class CameraSwitch : MonoBehaviour {

	public Camera cameraMain;
	public Camera cameraCutScene;

    menuScript menu;

	bool isPlaying = false;
	bool isInGame = false;

	PlayerMovement sBroMovement;
	PlayerMovement bBroMovement;

	// Use this for initialization
	void Start () {
		sBroMovement = GameObject.FindGameObjectWithTag ("Small").GetComponent<PlayerMovement> ();
		bBroMovement = GameObject.FindGameObjectWithTag ("Big").GetComponent<PlayerMovement> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isPlaying && (Input.GetButtonDown ("SMALL_INTERACT_1") || Input.GetButtonDown ("BIG_INTERACT_1")) && !isInGame) {
			Skip();
		}
    }

	public void Play () {
		sBroMovement.enabled = false;
		GameObject.FindGameObjectWithTag ("Small").GetComponent<SBGrounded> ().cutScene = true;
		bBroMovement.enabled = false;
		GameObject.FindGameObjectWithTag ("Small").GetComponentInChildren<FireAttackScript> ().enabled = false;
		cameraCutScene.enabled = true;
		cameraCutScene.GetComponent<Animation>().Play();
		isPlaying = true;
		Invoke("SwitchToMain", 6.1f);
	}

	void Skip () {
		isInGame = true;
		cameraCutScene.GetComponent<Animation>().Stop();
		SwitchToMain();
	}

	void SwitchToMain () {
		isInGame = true;
		cameraCutScene.enabled = false;
		cameraMain.enabled = true;

		bBroMovement.GetComponent<PlayerMovement> ().enabled = true;
		GameObject.FindGameObjectWithTag ("Small").GetComponent<SBGrounded> ().cutScene = false;
		sBroMovement.GetComponent<PlayerMovement> ().enabled = true;
		GameObject.FindGameObjectWithTag ("Small").GetComponentInChildren<FireAttackScript> ().enabled = true;
	}
}
