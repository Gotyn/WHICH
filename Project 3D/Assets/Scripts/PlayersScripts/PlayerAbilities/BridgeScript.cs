using UnityEngine;
using System.Collections;

public class BridgeScript : MonoBehaviour {

	
	GameObject small;
	bool SBroInPos = false;
    bool activated = false;
	Animator anim;
	[SerializeField]
	GameObject bridge;

	// Use this for initialization
	void Start () {
		small = GameObject.FindGameObjectWithTag("Small");
		
		anim = small.GetComponentInChildren<Animator>();
        bridge.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButton("SMALL_INTERACT_1"))
        { //pulling bigbro
            if (SBroInPos && !activated)
            {
                activated = true;
                anim.SetBool("StartedLift", true);
                anim.SetBool("StoppedLift", false);
                StartCoroutine(Wait());
                bridge.SetActive(true);
            }

        }
        else
        {
            if (activated)
            {
                //  anim.SetBool("Lifting", false);
             
                bridge.SetActive(false);
                activated = false;
                anim.SetBool("StoppedLift", true);
            }

        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("StartedLift", false);
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
