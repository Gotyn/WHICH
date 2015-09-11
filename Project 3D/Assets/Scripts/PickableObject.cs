﻿using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to every pickupable item*.
/// 
/// *not on the small player tho'.
/// </summary>


[RequireComponent(typeof(AudioSource))]

public class PickableObject : MonoBehaviour {

    private Vector3 startPos;
    private Camera cam;
    private ParticleSystem glow;
    private AudioSource audioThud;
    private bool previousOnGround = false;
    public bool pickable = false;

    void Start () {
		startPos = this.transform.position;
		cam = Camera.main;
		glow = GetComponentInChildren<ParticleSystem> ();
		glow.gameObject.SetActive (false);

        audioThud = GetComponent<AudioSource>();
	}

	void Update () {
        Debug.Log("previousOnGround  " + previousOnGround );
        Debug.Log("onground()  " + OnGround());

        CamControl();

        if (OnGround() && !previousOnGround) {
            if (audioThud != null && !audioThud.isPlaying) {
                audioThud.Play();
                previousOnGround = true;
                
            }
        }
	}

	public void Glow(bool on){
		if (on) {
			glow.gameObject.SetActive (true);
		} else {
			glow.gameObject.SetActive (false);
		}
	}
	
	//check if player is outside screen pos
	void CamControl()
	{
		Vector3 screenCoord = cam.WorldToScreenPoint(this.transform.position);
		
		if (screenCoord.x < -20 || screenCoord.x > Screen.width + 20 || screenCoord.y < -20 || screenCoord.y > Screen.height + 20)
		{
			transform.position = startPos;
		}
	}

    bool OnGround() {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -transform.up, out hit, transform.lossyScale.y / 2 + 0.05f)) {
            if (hit.transform.CompareTag("TestGround")) {
                return true;
            } else {
                return false;
            }
        }
        else {
            previousOnGround = false;
            return false;
        }
    }

}
