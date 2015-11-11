using UnityEngine;
using System.Collections;

public class SoccerTriggerScript : MonoBehaviour {

	bool smallBroInPos = false;
	bool bigBroInPos = false;

	AudioSource cheeringSound;

	void Start () {
		cheeringSound = GetComponent<AudioSource> ();
	}

	void Update () {
		if ((smallBroInPos || bigBroInPos) && !cheeringSound.isPlaying) {
			cheeringSound.Play();
		} else if (!smallBroInPos && !bigBroInPos && cheeringSound.isPlaying) {
			cheeringSound.Stop();
		}
	}

	void OnTriggerEnter (Collider other){
		if (other.CompareTag ("Small")) {
			smallBroInPos = true;
			other.GetComponentInChildren<FireAttackScript>().enabled = false;
		}
		if (other.CompareTag ("Big")) {
			bigBroInPos = true;
			other.GetComponentInChildren<HolderTest>().enabled = false;
		}
	}

	void OnTriggerExit (Collider other){
		if (other.CompareTag ("Small")) {
			smallBroInPos = false;
			other.GetComponentInChildren<FireAttackScript>().enabled = true;
		}
		if (other.CompareTag ("Big")) {
			bigBroInPos = false;
			other.GetComponentInChildren<HolderTest>().enabled = true;
		}
	}
}
