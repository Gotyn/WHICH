using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LogSoundsScript : MonoBehaviour {

	public List<AudioClip> audios;
	AudioSource source;

	bool impactSound = true;

	void Start () {
		source = GetComponent<AudioSource> ();
		source.clip = audios[0];
	}
	void Update () {
		Debug.Log (impactSound);
	}

	void OnCollisionEnter (Collision other) {
		if (other.collider.CompareTag ("TestGround") && !source.isPlaying && impactSound) {
			source.clip = audios[Random.Range(0,3)];
			Debug.Log ("TestGround");
			source.Play ();
			impactSound = false;

		}
	}
	void OnCollisionExit (Collision other) {
		if (other.collider.CompareTag ("TestGround")) {
			impactSound = true;
		}
	}
}
