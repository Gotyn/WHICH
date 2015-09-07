using UnityEngine;
using System.Collections;

public class ZoomInScript : MonoBehaviour {

	bool canRead = false;
	bool zoomedIn = false;

	Camera cam;

	[SerializeField]
	int index;

	[SerializeField]
	int previousIndex;

	GameObject small;
	GameObject big;

	void Start () {
		small = GameObject.FindGameObjectWithTag("Small");
		big = GameObject.FindGameObjectWithTag("Big");
		cam = Camera.main;
	}
	// Use this for initialization

	// Update is called once per frame
	void Update () {
		Debug.Log (zoomedIn);
		ZoomIn ();
	}

	void ZoomIn () {
		if (canRead && (Input.GetButtonDown ("Interact_Small_1") || Input.GetButtonDown ("Interact_Big_1")) && !zoomedIn) {
			big.GetComponent<BigBroMovement>().enabled = false;
			big.GetComponent<CameraControlScript>().enabled = false;
			small.GetComponent<SmallBroMovement>().enabled = false;
			small.GetComponent<CameraControlScript>().enabled = false;
			cam.GetComponent<CameraSpline>().MoveTo(index);
			Invoke("SetBoolTrue",0.5f);
		}

		if (canRead && (Input.GetButtonDown("Interact_Small_1") || Input.GetButtonDown("Interact_Big_1")) && zoomedIn) {
			big.GetComponent<BigBroMovement>().enabled = true;
			small.GetComponent<SmallBroMovement>().enabled = true;
			cam.GetComponent<CameraSpline>().MoveTo(previousIndex);
			zoomedIn = false;
			Invoke("SetFunctionsActive",0.5f);
			
		}
	}

	void SetBoolTrue () {
		zoomedIn = true;
	}

	void SetFunctionsActive () {
		big.GetComponent<CameraControlScript>().enabled = true;
		small.GetComponent<CameraControlScript>().enabled = true;
	}

	void OnTriggerEnter (Collider hit) {
		if (hit.transform.CompareTag ("Small") || hit.transform.CompareTag ("Big")) {
			canRead = true;
			small.GetComponentInChildren<FireAttackScript>().canRead = true;
		}
	}

	void OnTriggerExit (Collider hit) {
		if (hit.transform.CompareTag ("Small") || hit.transform.CompareTag ("Big")) {
			small.GetComponentInChildren<FireAttackScript>().canRead = false;
			canRead = false;
		}
	}
}
