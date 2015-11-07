using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to every pickupable item*.
/// 
/// *not on the small player tho'.
/// </summary>


[RequireComponent(typeof(AudioSource))]

public class PickableObject : MonoBehaviour {

	//transform
    private Vector3 startPos;

	//camera
    private Camera cam;

	//particle
    private ParticleSystem glow;

	//audio
    private AudioSource audioThud;
	private bool releaseAudio = false;
    private bool previousOnGround = true;

    public bool pickable = false;

    void Start () {
		startPos = this.transform.position;

		cam = Camera.main;

		glow = GetComponentInChildren<ParticleSystem> ();
        glow.enableEmission = false;

        audioThud = GetComponent<AudioSource>();
		Invoke ("ReleaseAudio", 5); // Fix for boxdrop sound on level start.
	}

	void Update () {
        if (gameObject.CompareTag("Box"))
        {
            if (releaseAudio && OnGround() && !previousOnGround)
            {
                if (audioThud != null && !audioThud.isPlaying)
                {
                    audioThud.Play();
                    previousOnGround = true;
                }
            }
        }
	}

	public void Glow(bool on){
		if (on) {
            glow.enableEmission = true;
		} else {
            glow.enableEmission = false;
		}
	}

	void ReleaseAudio () {
		releaseAudio = true;
	}

	//check if pickable object is on ground with raycast
    bool OnGround() {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -transform.up, out hit, transform.lossyScale.y / 2 + 0.05f)) {
            if ((hit.transform.CompareTag("TestGround") || hit.transform.CompareTag("Box"))) {
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
