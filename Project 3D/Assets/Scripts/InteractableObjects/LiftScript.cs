using UnityEngine;
using System.Collections;

public class LiftScript : MonoBehaviour {


    Animator animator;
	[SerializeField]
	GameObject liftable;
    [SerializeField]
    Transform endPosition;

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
        endpos = endPosition.position;
        animator = smallInput.GetComponentInChildren<Animator>();
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
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("StartedLift", false);
        animator.SetBool("Lifting", true);
    }
	// lifts the liftable object
	void Lift () {
		if (canLift && Input.GetButton (smallInput.interactControl_1)) {
            animator.SetBool("StartedLift", true);
            StartCoroutine(Wait());
			rigibody.isKinematic = true;
			rigibody.useGravity = false;
			
			if (Vector3.Distance(liftable.transform.position, endpos) > 0.1f)
			{
				rigibody.MovePosition(liftable.transform.position + ((endpos - liftable.transform.position).normalized) * 5 * Time.deltaTime);
			}else {
                
				liftable.GetComponent<Rigidbody>().velocity = Vector3.zero;
			}
		} else {
            animator.SetBool("Lifting", false);
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
