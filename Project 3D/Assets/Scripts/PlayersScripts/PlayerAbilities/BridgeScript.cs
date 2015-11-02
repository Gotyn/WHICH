using UnityEngine;
using System.Collections;

public class BridgeScript : MonoBehaviour {

	
	GameObject small;
	bool SBroInPos = false;
	Animator anim;
	[SerializeField]
	GameObject bridge;
	// Use this for initialization
	void Start () {
		small = GameObject.FindGameObjectWithTag("Small");
		
		anim = small.GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetButton ("SMALL_INTERACT_1") && SBroInPos) { //pulling bigbro
			anim.SetBool ("StartedLift", true);
			StartCoroutine (Wait ());
			bridge.SetActive (true);

			//small.GetComponentInChildren<SBGrounded> ().grabbing = true;
			//small.GetComponent<PlayerMovement> ().enabled = false;
		} else {
			anim.SetBool("Lifting", false);
			bridge.SetActive (false);

		}
	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds(0.1f);
		anim.SetBool("StartedLift", false);
		anim.SetBool("Lifting", true);

		
	}
	void OnTriggerEnter(Collider hit)
	{
		if (hit.gameObject.CompareTag ("SmallT")) {
			SBroInPos = true;
			small.GetComponentInChildren<FireAttackScript> ().canCast = false;
		}
	}

	void OnTriggerExit(Collider hit)
	{
		if (hit.gameObject.CompareTag ("SmallT")) {
			SBroInPos = false;
			small.GetComponentInChildren<FireAttackScript> ().canCast = true;
		}
	}
}
