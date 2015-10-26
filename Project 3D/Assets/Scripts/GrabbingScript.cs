using UnityEngine;
using System.Collections;

public class GrabbingScript : MonoBehaviour {
    
    //Components
    GameObject big;
    GameObject small;
    BigBroGlow bbGlow;
    Animator anim;
    //Variables
	bool move = false;

    // Use this for initialization
    void Start () {

        big = GameObject.FindGameObjectWithTag("Big");
        small = GameObject.FindGameObjectWithTag("Small");
        bbGlow = big.GetComponentInChildren<BigBroGlow>();
      //  Debug.Log("-------->" + small.gameObject.name);
        anim = small.GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (Input.GetButton ("SMALL_INTERACT_2") && bbGlow.bInPos && bbGlow.sInPos) { //pulling bigbro
            anim.SetBool("StartedLift", true);
            StartCoroutine(Wait());
			move = true;
			small.GetComponentInChildren<SBGrounded>().grabbing = true;
			big.GetComponent<PlayerMovement>().enabled = false;
			small.GetComponent<PlayerMovement>().enabled = false;
			big.GetComponent<Rigidbody>().isKinematic = true;
			big.GetComponent<Rigidbody>().useGravity = false;
		}

		if (Vector3.Distance(small.transform.position,big.transform.position) < 2f) {
            anim.SetBool("Lifting", false);
			move = false;
			small.GetComponentInChildren<SBGrounded>().grabbing = false;
			big.GetComponent<PlayerMovement>().enabled = true;
			big.GetComponent<Rigidbody>().isKinematic = false;
			big.GetComponent<Rigidbody>().useGravity = true;
		}

		if (move) {
            anim.SetBool("Lifting", true);
            big.GetComponent<Rigidbody>().MovePosition(big.transform.position + ((small.transform.position - big.transform.position).normalized) * 5 * Time.deltaTime);
		}
 
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("StartedLift", false);
        
    }
    void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.CompareTag("SmallT"))
            bbGlow.sInPos = true;
        if (hit.gameObject.CompareTag("BigT"))
            bbGlow.bInPos = true;
    }

    void OnTriggerExit(Collider hit)
    {
        if (hit.gameObject.CompareTag("SmallT"))
            bbGlow.sInPos = false;
        if (hit.gameObject.CompareTag("BigT"))
            bbGlow.bInPos = false;
    }
}
