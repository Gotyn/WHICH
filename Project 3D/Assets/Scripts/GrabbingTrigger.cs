using UnityEngine;
using System.Collections;

public class GrabbingTrigger: MonoBehaviour {

    public BigBroGlow bbGlow;

	void OnTriggerEnter(Collider hit) {
        if (hit.gameObject.CompareTag("SmallT"))
            bbGlow.sInPos = true;
        if (hit.gameObject.CompareTag("BigT"))
            bbGlow.bInPos = true;
    }
	
	void OnTriggerExit (Collider hit) {
        if (hit.gameObject.CompareTag("SmallT"))
            bbGlow.sInPos = false;
        if (hit.gameObject.CompareTag("BigT"))
            bbGlow.bInPos = false;
    }
}
