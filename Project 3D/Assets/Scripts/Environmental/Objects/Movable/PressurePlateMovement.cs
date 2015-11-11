using UnityEngine;
using System.Collections;

public class PressurePlateMovement : InteractableObjectMovement
{
	private AudioSource audioPlate;
	private int previousState;
    MenuScript menu;

	private bool releaseAudio = false;

	void Start()
    {
		audioPlate = GetComponent<AudioSource>();
        maxDistance = 0.05f;
        menu = MenuScript.Instance;
		Invoke ("ReleaseAudio", 5);

    }

	void Update () {
		ManageAudio ();
	}

	void ReleaseAudio () {
		releaseAudio = true;
	}

	void ManageAudio() {
		if (audioPlate != null && !audioPlate.isPlaying && previousState != state && releaseAudio) {
			if (state == 2 || state == 1) {
				if(!menu.mainMenuCanvas.enabled) audioPlate.Play();
				previousState = state;
			}
		}
	}
}