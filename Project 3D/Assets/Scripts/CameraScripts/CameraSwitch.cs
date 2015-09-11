using UnityEngine;
using System.Collections;

public class CameraSwitch : MonoBehaviour {

	public Camera cameraMain;
	public Camera cameraCutScene;

    menuScript menu;

	// Use this for initialization
	void Start () {
        menu = GameObject.Find("mainMenu").GetComponent<menuScript>();
		GameObject.FindGameObjectWithTag ("Big").GetComponent<PlayerMovement> ().enabled = false;
		GameObject.FindGameObjectWithTag ("Small").GetComponent<PlayerMovement> ().enabled = false;
		
		


	}
	
	// Update is called once per frame
	void Update () {
        if (menu.clicker && !menu.timeOut)
        {
            cameraCutScene.enabled = true;
            cameraCutScene.GetComponent<Animation>().Play();
            Invoke("SwitchToMain", 6.1f);
            

        }

    }

	void SwitchToMain () {
		cameraMain.enabled = true;
		cameraCutScene.enabled = false;
		GameObject.FindGameObjectWithTag ("Big").GetComponent<PlayerMovement> ().enabled = true;
		GameObject.FindGameObjectWithTag ("Small").GetComponent<PlayerMovement> ().enabled = true;
	}
}
