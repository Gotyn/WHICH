using UnityEngine;
using System.Collections;

public class BridgeScript : MonoBehaviour {
	GameObject small;
	bool smallBroInPos = false;
    bool activated = false;
	Animator anim;
	[SerializeField]
	GameObject bridge;

	void Start () {
		small = GameObject.FindGameObjectWithTag("Small");
		
		anim = small.GetComponentInChildren<Animator>();

        bridge.GetComponentInChildren<ParticleSystem>().enableEmission = false;
        bridge.GetComponent<Collider>().isTrigger = true;
    }

    void Update()
    {
        if (Input.GetButton("SMALL_INTERACT_1"))
        { 
            if (smallBroInPos && !activated)
            {
                activated = true;
                anim.SetBool("StartedLift", true);
                anim.SetBool("StoppedLift", false);
                StartCoroutine(Wait());
                bridge.GetComponentInChildren<ParticleSystem>().enableEmission = true;
                bridge.GetComponent<Collider>().isTrigger = false;
            }
        }
        else
        {
            if (activated)
            {
                bridge.GetComponentInChildren<ParticleSystem>().enableEmission = false;
                bridge.GetComponent<Collider>().isTrigger = true;
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
			smallBroInPos = true;
			small.GetComponentInChildren<FireAttackScript> ().canCast = false;
		}
	}

	void OnTriggerExit(Collider hit)
	{
		if (hit.gameObject.CompareTag ("SmallT")) {
			smallBroInPos = false;
			small.GetComponentInChildren<FireAttackScript> ().canCast = true;
		}
	}
}
