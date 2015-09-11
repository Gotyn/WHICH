using UnityEngine;
using System.Collections;

public class GrabbingScript : MonoBehaviour {

	public bool SmallBroInPos = false;
	public bool BigBroInPos = false;
	bool move = false;

	GameObject big;
	GameObject small;

	ParticleSystem glow;

    // Use this for initialization
    void Start () {
		big = GameObject.FindGameObjectWithTag ("Big");
		small = GameObject.FindGameObjectWithTag ("Small");
		glow = big.GetComponentInChildren<ParticleSystem> ();
	//	glow.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (SmallBroInPos && BigBroInPos) { //pulling bigbro
			Glow (true);
		} else {
			Glow (false);
		}

		if (Input.GetButton ("SMALL_INTERACT_2") && SmallBroInPos && BigBroInPos) { //pulling bigbro
			move = true;
			small.GetComponent<CheckIfGrounded>().grabbing = true;
			big.GetComponent<PlayerMovement>().enabled = false;
			small.GetComponent<PlayerMovement>().enabled = false;
			big.GetComponent<Rigidbody>().isKinematic = true;
			big.GetComponent<Rigidbody>().useGravity = false;
		}

		if (Vector3.Distance(small.transform.position,big.transform.position) < 2f) {
			move = false;
			small.GetComponent<CheckIfGrounded>().grabbing = false;
			big.GetComponent<PlayerMovement>().enabled = true;
			big.GetComponent<Rigidbody>().isKinematic = false;
			big.GetComponent<Rigidbody>().useGravity = true;
		}

		if (move) {
			big.GetComponent<Rigidbody>().MovePosition(big.transform.position + ((small.transform.position - big.transform.position).normalized) * 5 * Time.deltaTime);
		}
	}

	public void Glow(bool on){
		if (on) {
			glow.gameObject.SetActive (true);
		} else {
			glow.gameObject.SetActive (false);
		}
	}

	void OnTriggerEnter(Collider hit) {
		if (hit.gameObject.CompareTag ("Small")) {
			SmallBroInPos = true;
		}
		if (hit.gameObject.CompareTag ("Big")) {
			BigBroInPos = true;
		}
	}

	void OnTriggerExit (Collider hit) {
		if (hit.gameObject.CompareTag ("Small")) {
			SmallBroInPos = false;
		}
		if (hit.gameObject.CompareTag ("Big")) {
			BigBroInPos = false;
		}
	}
}
