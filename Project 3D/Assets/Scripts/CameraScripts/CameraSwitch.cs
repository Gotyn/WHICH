using UnityEngine;
using System.Collections;

public class CameraSwitch : MonoBehaviour {

	public Camera cameraMain;
	public Camera cameraCutScene;

    menuScript menu;

	bool isPlaying = false;
	bool isInGame = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (isPlaying && (Input.GetButtonDown ("SMALL_INTERACT_1") || Input.GetButtonDown ("BIG_INTERACT_1")) && !isInGame) {
			Skip();
		}

//        if (menu.clicker && !isPlaying )
//        {
//            cameraCutScene.enabled = true;
//            cameraCutScene.GetComponent<Animation>().Play();
//			isPlaying = true;
//            Invoke("SwitchToMain", 6.1f);
//        }

    }

	public void Play () {
		GameObject.FindGameObjectWithTag ("Big").GetComponent<PlayerMovement> ().enabled = false;
		GameObject.FindGameObjectWithTag ("Small").GetComponent<PlayerMovement> ().enabled = false;
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

		GameObject.FindGameObjectWithTag ("Big").GetComponent<PlayerMovement> ().enabled = true;
		GameObject.FindGameObjectWithTag ("Small").GetComponent<PlayerMovement> ().enabled = true;
	}
}
