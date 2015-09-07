using UnityEngine;
using System.Collections;




public class BigGrabbingScript: MonoBehaviour {

	public GrabbingScript script;

	void OnTriggerEnter () {
		script.BigBroInPos = true;
	}

	void OnTriggerExit () {
		script.BigBroInPos = false;
	}
}
