using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CheckpointScript : MonoBehaviour {

    GameObject small;
    Camera cam;
    GameObject big;

	Text text;

	public GameObject invWall;

	public GameObject newSpawnPointSmall;
	public GameObject newSpawnPointBig;

	bool smallEntered = false;
	bool bigEntered = false;
    
	// Use this for initialization
	void Start () {
		text = GameObject.Find ("CheckpointText").GetComponent<Text> ();
		small = GameObject.FindGameObjectWithTag ("Small");
		big = GameObject.FindGameObjectWithTag ("Big");
        cam = Camera.main;
    }

	void Update () {
		if (smallEntered && bigEntered) {
			invWall.SetActive (false);
			MoveToNext ();
			text.enabled = false;
		} else if (smallEntered || bigEntered) {
			text.enabled = true;
		} else if (!smallEntered && !bigEntered) {

		}
	}

	void MoveToNext (){
		small.GetComponent<CameraControlScript> ().spawn = newSpawnPointSmall;
		big.GetComponent<CameraControlScript> ().spawn = newSpawnPointBig;
		cam.GetComponent<CameraSpline> ().MoveToNext ();
		this.gameObject.SetActive (false);
	}
	
    // Update is called once per frame
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
			text.enabled = false;
		}
		
		if (hit.transform.CompareTag ("Big")) {
			bigEntered = false;
			text.enabled = false;
		}
	}
}
