using UnityEngine;
using System.Collections;

public class HandleScript : MonoBehaviour {

	private ParticleSystem glow;

	private PlayerInputScript bigInput;

	Animator animator;

	[HideInInspector]
	public bool canHandle = false;
	[HideInInspector]
	public bool isOpen = false;
	[HideInInspector]
	public bool completed = false;

	public bool needActivated = false; 

	// Use this for initialization
	void Start () {
		animator = GetComponentInChildren<Animator> (); 
		glow = GetComponentInChildren<ParticleSystem> ();
		glow.enableEmission = false;
		bigInput = GameManagerScript.BB.GetComponent<PlayerInputScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		if ((Input.GetButtonDown (bigInput.interactControl_1) ||  DPadButtons.down) && canHandle) {  
			PullLever();
		}
	}

	void PullLever () {
		if (!isOpen) {
			animator.SetBool ("Opened", true);
			isOpen = true;
		} else {
			animator.SetBool ("Opened", false);
			isOpen = false;
		}
	}

	public void Glow(bool on){
		if (on) {
			glow.enableEmission = true;
		} else {
			glow.enableEmission = false;
		}
	}
}
