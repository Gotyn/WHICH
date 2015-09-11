using UnityEngine;
using System.Collections;

public class BBGlowScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

		Invoke ("Disable", 0.2f);
	}

	void Disable () {
		this.gameObject.SetActive (false);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
