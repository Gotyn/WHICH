using UnityEngine;
using System.Collections;

public class PressurePlateMovement : InteractableObjectMovement
{

	private AudioSource audioPlate;
	private int previousState;

	void Start()
    {
		audioPlate = GetComponent<AudioSource>();
        maxDistance = 0.05f;
    }

	void Update () {
		ManageAudio ();
	}

	void PlaySound () {
		GetComponent<AudioSource> ().Play ();
	}

	void ManageAudio() {
		if (audioPlate != null && !audioPlate.isPlaying && previousState != state) {
			if (state == 2 || state == 1) {
				audioPlate.Play();
				previousState = state;
			}
			  //set currentstate as previousstate to prevent soundloop.
		}
	}
}