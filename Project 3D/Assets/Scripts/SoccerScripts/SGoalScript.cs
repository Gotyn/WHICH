using UnityEngine;
using System.Collections;

public class SGoalScript : MonoBehaviour {

	public bool bGoal= false;

	GameObject soccerManager;
		
	GameObject bigBro;
	GameObject smallBro;

	void Start () {
		soccerManager = GameObject.Find("SoccerGame");
        bigBro = GameManagerScript.BB;
        smallBro = GameManagerScript.SB;
    }

	void OnTriggerEnter (Collider other) {
		if (other.CompareTag ("Ball")) {
			if (bGoal) {
				soccerManager.GetComponent<ScoreScript>().bigBroScore++;
			} else{
				soccerManager.GetComponent<ScoreScript>().smallBroScore++;
			}
			bigBro.GetComponent<CameraControlScript>().Respawn();
			smallBro.GetComponent<CameraControlScript>().Respawn();
			soccerManager.GetComponent<ScoreScript>().Respawn();
		}
	}
}
