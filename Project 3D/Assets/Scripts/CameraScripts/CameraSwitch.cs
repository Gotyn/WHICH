using UnityEngine;
using System.Collections;

public class CameraSwitch : MonoBehaviour {

	public Camera cameraMain;
	public Camera cameraCutScene;
	bool isPlaying = false;

	// Use this for initialization
	void Start () {
		GameObject.FindGameObjectWithTag ("Big").GetComponent<PlayerMovement> ().enabled = false;
		GameObject.FindGameObjectWithTag ("Small").GetComponent<PlayerMovement> ().enabled = false;
		GameObject.FindGameObjectWithTag ("Small").GetComponentInChildren<FireAttackScript> ().enabled = false;
		cameraCutScene.enabled = true;
		cameraCutScene.GetComponent<Animation> ().Play ();
		isPlaying = true;
		Invoke ("SwitchToMain", 6.1f);

	

	}
	
	// Update is called once per frame
	void Update () {
	
		if (isPlaying && (Input.GetButtonDown ("SMALL_INTERACT_1") || Input.GetButtonDown ("BIG_INTERACT_1"))) {
			cameraCutScene.GetComponent<Animation> ().Stop ();
			SwitchToMain();
		}
	}

	void SwitchToMain () {
		cameraMain.enabled = true;
		cameraCutScene.enabled = false;
		GameObject.FindGameObjectWithTag ("Big").GetComponent<PlayerMovement> ().enabled = true;
		GameObject.FindGameObjectWithTag ("Small").GetComponent<PlayerMovement> ().enabled = true;
		GameObject.FindGameObjectWithTag ("Small").GetComponentInChildren<FireAttackScript> ().enabled = true;
	}
}
