using UnityEngine;
using System.Collections;

public class GrabbingScript : MonoBehaviour {

	bool SmallBroInPos = false;
	public bool BigBroInPos = false;
	bool move = false;

	GameObject big;
	GameObject small;

    PlayerInputScript smallInput;

    // Use this for initialization
    void Start () {
		big = GameObject.FindGameObjectWithTag ("Big");
		small = GameObject.FindGameObjectWithTag ("Small");
        smallInput = small.GetComponent<PlayerInputScript>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetButton (smallInput.interactControl_2) && SmallBroInPos && BigBroInPos) { //pulling bigbro
			move = true;
			small.GetComponent<CheckIfGrounded>().grabbing = true;
			big.GetComponent<PlayerMovement>().enabled = false;
			small.GetComponent<PlayerMovement>().enabled = false;
			big.GetComponent<Rigidbody>().isKinematic = true;
			big.GetComponent<Rigidbody>().useGravity = false;
		}

		if (Vector3.Distance(small.transform.position,big.transform.position) < 1f) {
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

	void OnTriggerEnter(Collider hit) {
		if (hit.gameObject.CompareTag ("Small")) {
			SmallBroInPos = true;
		}
	}

	void OnTriggerExit (Collider hit) {
		if (hit.gameObject.CompareTag ("Small")) {
			SmallBroInPos = false;
		}
	}
}
