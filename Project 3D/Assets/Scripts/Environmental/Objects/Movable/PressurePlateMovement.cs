using UnityEngine;
using System.Collections;

public class PressurePlateMovement : InteractableObjectMovement
{
	private AudioSource audioPlate;
	private int previousState;
    MenuScript menu;

	void Start()
    {
		audioPlate = GetComponent<AudioSource>();
        maxDistance = 0.05f;
        menu = MenuScript.Instance;

    }

	void Update () {
		ManageAudio ();
	}

	void ManageAudio() {
		if (audioPlate != null && !audioPlate.isPlaying && previousState != state) {
			if (state == 2 || state == 1) {
				if(!menu.mainMenuCanvas.enabled) audioPlate.Play();
				previousState = state;
			}
		}
	}
}