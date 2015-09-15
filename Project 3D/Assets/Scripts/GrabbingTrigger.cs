using UnityEngine;
using System.Collections;

public class GrabbingTrigger: MonoBehaviour {

    BigBroGlow bbGlow;
    void Start()
    {
        bbGlow = FindObjectOfType(typeof(BigBroGlow)) as BigBroGlow;
    }
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
