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
    DialogueScript dialogue;
    CameraSwitch cameraSwitch;

    bool smallEntered = false;
	bool bigEntered = false;
    public int playDialogue;
    public float delayDialogue = 2f;

	bool used = false;
    [Tooltip("Normal-ROTATION- 45, 270, 0\nSoccerfield-ROTATION- 45, 0, 0\nDARK ROOM-ROTATION- 45, 180, 0")]
    public Quaternion newRotation;
    [Tooltip("Normal-OFFSET- 20, 20, 0\nSoccerfield-OFFSET- 0, 20, -20\nDARK ROOM-OFFSET- 0, 20, 20")]
    public Vector3 newOffset;
    Quaternion oldRot;
    Quaternion cameraRotation;

	// Use this for initialization
	void Start () {
		text = GameObject.Find("CheckpointText").GetComponent<Image> ();
		small = GameObject.FindGameObjectWithTag ("Small");
		big = GameObject.FindGameObjectWithTag ("Big");
        cam = Camera.main;
        gameManager = FindObjectOfType<GameManagerScript>();
        cameraSwitch = Camera.main.GetComponent<CameraSwitch>();
        dialogue = gameManager.GetComponent<DialogueScript>();
        
    }

	void Update () {
		if (smallEntered && bigEntered && !dialogue.chat.enabled) {
			invWall.SetActive (false);
			if (!used)MoveToNext ();
			text.enabled = false;
		} else if ((smallEntered || bigEntered)&& !used) {
			text.enabled = true;
		} 
	}

	void MoveToNext (){
//        Debug.Log("hallo");
        gameManager.currentPuzzle++;
       // Debug.Log("PUZZLE NUMBER --- " + gameManager.currentPuzzle);
		small.GetComponent<CameraControlScript> ().spawn = newSpawnPointSmall;
		big.GetComponent<CameraControlScript> ().spawn = newSpawnPointBig;
        
        //----------------------------------------------
		Camera.main.GetComponent<BetterCameraScript> ().offset = newOffset;
		Camera.main.GetComponent<BetterCameraScript> ().newRotation = Quaternion.Euler(newRotation.x, newRotation.y, newRotation.z);
		Camera.main.GetComponent<BetterCameraScript> ().startTime = Time.time;
		Camera.main.GetComponent<BetterCameraScript> ().length = 0.6f;
        //----------------------------------------------
	//	cam.GetComponent<CameraSpline> ().MoveToNext ();
        dialogue.StartCoroutine("Puzzle_" + playDialogue.ToString(), 2f);
		used = true;

	}

	void Disable () {
		this.gameObject.SetActive (false);

	}
	
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
