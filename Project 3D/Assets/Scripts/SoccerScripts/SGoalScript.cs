using UnityEngine;
using System.Collections;

public class SGoalScript : MonoBehaviour {

	public bool bGoal= false;

	GameObject soccerManager;
		
	GameObject big;
	GameObject small;

	void Start () {
		soccerManager = GameObject.Find("SoccerGame");
		big = GameObject.FindGameObjectWithTag ("Big");
		small = GameObject.FindGameObjectWithTag ("Small");
	}

	void OnTriggerEnter (Collider other) {
		if (other.CompareTag ("Ball")) {
			if (bGoal) {
				soccerManager.GetComponent<ScoreScript>().bScore++;
			} else{
				soccerManager.GetComponent<ScoreScript>().sScore++;
			}
			big.GetComponent<CameraControlScript>().Respawn();
			small.GetComponent<CameraControlScript>().Respawn();
			soccerManager.GetComponent<ScoreScript>().Respawn();

		}

	}
}
