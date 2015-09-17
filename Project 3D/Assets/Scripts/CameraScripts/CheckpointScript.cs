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

    GameManagerScript gameManager;
    dialogScript dialog;
    CameraSwitch cameraSwitch;

    bool smallEntered = false;
	bool bigEntered = false;
    public int playDialog;
    
    
	// Use this for initialization
	void Start () {
		text = GameObject.Find ("CheckpointText").GetComponent<Text> ();
		small = GameObject.FindGameObjectWithTag ("Small");
		big = GameObject.FindGameObjectWithTag ("Big");
        cam = Camera.main;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        cameraSwitch = Camera.main.GetComponent<CameraSwitch>();
        dialog = gameManager.GetComponent<dialogScript>();
    }

	void Update () {
		if (smallEntered && bigEntered && !dialog.chat.enabled) {
			invWall.SetActive (false);
			MoveToNext ();
			text.enabled = false;
		} else if (smallEntered || bigEntered) {
			text.enabled = true;
		} else if (!smallEntered && !bigEntered) {

		}

        Debug.Log("Checkpoint: " + gameManager.currentPuzzle);
	}

	void MoveToNext (){
        gameManager.currentPuzzle++;
        Debug.Log("PUZZLE NUMBER --- " + gameManager.currentPuzzle);
		small.GetComponent<CameraControlScript> ().spawn = newSpawnPointSmall;
		big.GetComponent<CameraControlScript> ().spawn = newSpawnPointBig;
		cam.GetComponent<CameraSpline> ().MoveToNext ();
		this.gameObject.SetActive (false);

        dialog.StartCoroutine("Puzzle_" + gameManager.currentPuzzle.ToString());
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
