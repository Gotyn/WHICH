using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CheckpointScript : MonoBehaviour {

    GameObject small;
    Camera cam;
    GameObject big;

	Image text;
	public GameObject invWall;
	public GameObject newSpawnPointSmall;
	public GameObject newSpawnPointBig;

    GameManagerScript gameManager;
    dialogScript dialog;
    CameraSwitch cameraSwitch;

    bool smallEntered = false;
	bool bigEntered = false;
    public int playDialog;
    public float delayDialog = 2f;

	bool used = false;
    
    
	// Use this for initialization
	void Start () {
		text = GameObject.Find ("CheckpointText").GetComponent<Image> ();
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
			if (!used)MoveToNext ();
			text.enabled = false;
		} else if ((smallEntered || bigEntered)&& !used) {
			text.enabled = true;
		} 

        Debug.Log("Checkpoint: " + gameManager.currentPuzzle);
	}

	void MoveToNext (){
        gameManager.currentPuzzle++;
        Debug.Log("PUZZLE NUMBER --- " + gameManager.currentPuzzle);
		small.GetComponent<CameraControlScript> ().spawn = newSpawnPointSmall;
		big.GetComponent<CameraControlScript> ().spawn = newSpawnPointBig;
		cam.GetComponent<CameraSpline> ().MoveToNext ();
		//Invoke ("Disable", 0.2f);
        dialog.StartCoroutine("Puzzle_" + playDialog.ToString(), 2f);
		used = true;

	}

	void Disable () {
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
