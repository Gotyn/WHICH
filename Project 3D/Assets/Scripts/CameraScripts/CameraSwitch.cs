using UnityEngine;
using System.Collections;

public class CameraSwitch : MonoBehaviour {

	public Camera cameraMain;
	public Camera cameraCutScene;

	// Use this for initialization
	void Start () {
		GameObject.FindGameObjectWithTag ("Big").GetComponent<PlayerMovement> ().enabled = false;
		GameObject.FindGameObjectWithTag ("Small").GetComponent<PlayerMovement> ().enabled = false;
		cameraCutScene.enabled = true;
		cameraCutScene.GetComponent<Animation> ().Play ();
		Invoke ("SwitchToMain", 6.1f);

	

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SwitchToMain () {
		cameraMain.enabled = true;
		cameraCutScene.enabled = false;
		GameObject.FindGameObjectWithTag ("Big").GetComponent<PlayerMovement> ().enabled = true;
		GameObject.FindGameObjectWithTag ("Small").GetComponent<PlayerMovement> ().enabled = true;
	}
}
