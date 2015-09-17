using UnityEngine;
using System.Collections;

public class SoccerTriggerScript : MonoBehaviour {

	void OnTriggerEnter (Collider other){
		if (other.CompareTag ("Small")) {
			other.GetComponentInChildren<FireAttackScript>().enabled = false;
		}
		if (other.CompareTag ("Big")) {
			other.GetComponentInChildren<HolderTest>().enabled = false;
		}
	}

	void OnTriggerExit (Collider other){
		if (other.CompareTag ("Small")) {
			other.GetComponentInChildren<FireAttackScript>().enabled = true;
		}
		if (other.CompareTag ("Big")) {
			other.GetComponentInChildren<HolderTest>().enabled = true;
		}
	}
}
