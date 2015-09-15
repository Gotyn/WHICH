using UnityEngine;
using System.Collections;

public class LiftScript : MonoBehaviour {

	[SerializeField]
	GameObject liftable;


	Vector3 endpos;

	Rigidbody rigibody;

	bool canLift = false;

    private PlayerInputScript smallInput;

	ParticleSystem glow;

	void Start () {
		rigibody = liftable.GetComponent<Rigidbody> ();
        smallInput = GameObject.FindGameObjectWithTag("Small").GetComponent<PlayerInputScript>();
		glow = GetComponentInChildren<ParticleSystem> ();
        Glow(false);
        Debug.Log(liftable.transform.lossyScale.y);

        endpos = liftable.transform.lossyScale;

    }
	
	void FixedUpdate () {
		Lift ();
	}

	public void Glow(bool on){
		if (on) {
            glow.enableEmission = true;
		} else {
            glow.enableEmission = false;
		}
	}

	// lifts the liftable object
	void Lift () {
		if (canLift && Input.GetButton (smallInput.interactControl_1)) {
			rigibody.isKinematic = true;
			rigibody.useGravity = false;
			
			if (Vector3.Distance(liftable.transform.position, endpos) > 0.1f)
			{
				rigibody.MovePosition(liftable.transform.position + ((endpos - liftable.transform.position).normalized) * 120 * Time.deltaTime);
			}else {
				liftable.GetComponent<Rigidbody>().velocity = Vector3.zero;
			}
		} else {
			rigibody.isKinematic = false;
			rigibody.useGravity = true;
		}
	}

	void OnTriggerEnter(Collider hit) {
		if (hit.transform.CompareTag ("Small")) {
			hit.gameObject.GetComponentInChildren<FireAttackScript>().canCast = false;
			canLift = true;
			Glow(true);
		}
	}
	void OnTriggerExit( Collider hit) {
		if (hit.transform.CompareTag ("Small")) {
			hit.gameObject.GetComponentInChildren<FireAttackScript>().canCast = true;
			canLift = false;
			Glow(false);
		}
	}
}
