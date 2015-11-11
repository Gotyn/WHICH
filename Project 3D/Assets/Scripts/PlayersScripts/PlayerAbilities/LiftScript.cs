using UnityEngine;
using System.Collections;

public class LiftScript : MonoBehaviour {

	GameObject small; 

    Animator animator;
	[SerializeField]
	GameObject liftable;
    [SerializeField]
    Transform endPosition;

	Vector3 endpos;

	Rigidbody rigibody;

	bool canLift = false;
    bool itIsSet = false;
    private PlayerInputScript smallInput;

	ParticleSystem glow;

	//audio
	AudioSource liftSound;

	void Start () {
		small = GameManagerScript.SB;
		rigibody = liftable.GetComponent<Rigidbody> ();
        smallInput = GameManagerScript.SB.GetComponent<PlayerInputScript>();
		glow = GetComponentInChildren<ParticleSystem> ();
        Glow(false);
        endpos = endPosition.position;
        animator = smallInput.GetComponentInChildren<Animator>();
		liftSound = GetComponent<AudioSource> ();
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
       
    }
	// lifts the liftable object
	void Lift () {
		if (canLift && Input.GetButton (smallInput.interactControl_1)) {
			small.GetComponentInChildren<SBGrounded>().lifting = true;
			//small.GetComponent<PlayerMovement>().enabled = false;

            itIsSet = false;
            animator.SetBool("StartedLift", true);
            animator.SetBool("StoppedLift", false);
            StartCoroutine(Wait());
			rigibody.isKinematic = true;
			rigibody.useGravity = false;

			if (!liftSound.isPlaying){
				liftSound.Play();
			}
			
			if (Vector3.Distance(liftable.transform.position, endpos) > 0.1f)
			{
				rigibody.MovePosition(liftable.transform.position + ((endpos - liftable.transform.position).normalized) * 5 * Time.deltaTime);
			} else {
                
				liftable.GetComponent<Rigidbody>().velocity = Vector3.zero;
			}
		} else {
			small.GetComponentInChildren<SBGrounded>().lifting = false;
		//	small.GetComponent<PlayerMovement>().enabled = true;
            if (!itIsSet) { animator.SetBool("StoppedLift", true); itIsSet = true; }
            rigibody.isKinematic = false;
			rigibody.useGravity = true;
			if (liftSound.isPlaying){
				liftSound.Stop();
			}
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
