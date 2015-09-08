using UnityEngine;
using System.Collections;

public class LiftScript : MonoBehaviour {

	[SerializeField]
	GameObject liftable;

	[SerializeField]
	Transform endpos;

	Rigidbody rigibody;

	bool canLift = false;

    private PlayerInputScript smallInput;

	void Start () {
		rigibody = liftable.GetComponent<Rigidbody> ();
        smallInput = GameObject.FindGameObjectWithTag("Small").GetComponent<PlayerInputScript>();
    }
	
	void FixedUpdate () {
		Lift ();
	}

	// lifts the liftable object
	void Lift () {
		if (canLift && Input.GetButton (smallInput.interactControl_1)) {
			rigibody.isKinematic = true;
			rigibody.useGravity = false;
			
			if (Vector3.Distance(liftable.transform.position, endpos.position) > 0.1f)
			{
				rigibody.MovePosition(liftable.transform.position + ((endpos.position - liftable.transform.position).normalized) * 5 * Time.deltaTime);
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
		}
	}
	void OnTriggerExit( Collider hit) {
		if (hit.transform.CompareTag ("Small")) {
			hit.gameObject.GetComponentInChildren<FireAttackScript>().canCast = true;
			canLift = false;
		}
	}
}
