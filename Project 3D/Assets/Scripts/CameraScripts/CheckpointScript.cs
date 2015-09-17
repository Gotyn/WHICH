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

    dialogScript dialog;
    CameraSwitch cameraSwitch;

    private int puzzle = 1;

    bool smallEntered = false;
	bool bigEntered = false;
    
    
	// Use this for initialization
	void Start () {
		text = GameObject.Find ("CheckpointText").GetComponent<Text> ();
		small = GameObject.FindGameObjectWithTag ("Small");
		big = GameObject.FindGameObjectWithTag ("Big");
        cam = Camera.main;

        cameraSwitch = Camera.main.GetComponent<CameraSwitch>();
        dialog = GameObject.Find("GameManager").GetComponent<dialogScript>();
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
        puzzle++;
		small.GetComponent<CameraControlScript> ().spawn = newSpawnPointSmall;
		big.GetComponent<CameraControlScript> ().spawn = newSpawnPointBig;
		cam.GetComponent<CameraSpline> ().MoveToNext ();
		this.gameObject.SetActive (false);

        dialog.StartCoroutine("Puzzle_" + puzzle.ToString());
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
