using UnityEngine;
using System.Collections;

public class BigGrabbingScript: MonoBehaviour {

	public GrabbingScript script;

	void OnTriggerEnter(Collider hit) {
		if (hit.gameObject.CompareTag ("Big")) {
			script.BigBroInPos = true;
		}
	}

	void OnTriggerExit(Collider hit) {
		if (hit.gameObject.CompareTag ("Big")) {
			script.BigBroInPos = false;
		}
	}
}
