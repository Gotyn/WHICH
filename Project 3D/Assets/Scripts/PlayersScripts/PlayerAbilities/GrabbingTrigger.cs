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
            bbGlow.smallBroInPos = true;
        if (hit.gameObject.CompareTag("BigT"))
            bbGlow.bigBroInPos = true;
    }
	
	void OnTriggerExit (Collider hit) {
        if (hit.gameObject.CompareTag("SmallT"))
            bbGlow.smallBroInPos = false;
        if (hit.gameObject.CompareTag("BigT"))
            bbGlow.bigBroInPos = false;
    }
}
