using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to FireAttackTrigger (child from SmallBro).
/// 
/// It accesses CheckIfGrounded, to check if you are allowed to fire.
/// It accesses PlayerMovement, to disable it when firing.
/// It accesses PlayerInputScript, to get the correct interaction controls.
/// </summary>

public class FireAttackScript : MonoBehaviour {
	//Components
	[SerializeField]
    private GameObject prefabFire;
	[SerializeField]
    private GameObject prefabWater;
	[HideInInspector]
	public Transform torchTransform;

    private GameObject torch;

    //SmallBro's movement
    private PlayerMovement movement;
    private CheckIfGrounded checkGrounded;
    private Animator animator;
	
	float nextFireTime;
	float delayFire = 0.6f;

    //Variables
    [SerializeField]
    bool isInRange = false;

	public bool canCast = true;
	public bool canRead = false;

	public AudioClip fireAttack;
	public AudioClip waterAttack;

	AudioSource fireAttackSound;


	void Start() {
		movement = GetComponentInParent<PlayerMovement>();
        checkGrounded = transform.root.GetComponentInChildren<CheckIfGrounded>();
        animator = transform.root.GetComponentInChildren<Animator>();
		fireAttackSound = GetComponent<AudioSource> ();
	}

    void Update (){
		DoFireAttack ();
    }

	void DoFireAttack () {
        if (canShoot () && checkGrounded.Grounded && canCast && !canRead) {
            if (Input.GetButtonDown ("SMALL_INTERACT_1")) {  //fire
				GetComponentInParent<Rigidbody> ().velocity = Vector3.zero;
				movement.enabled = false;

                animator.SetBool("Casting", true);
                if (isInRange) {
					if (!torch.GetComponent<TorchScript> ().isLit) {
						CreateParticle (prefabFire);
						torch.GetComponent<TorchScript> ().SetFire ();
						if (!fireAttackSound.isPlaying){
							fireAttackSound.clip = fireAttack;
							fireAttackSound.Play();
						}
					} else {
						CreateParticle (prefabWater);
						torch.GetComponent<TorchScript> ().ExtinguishFire ();
						if (!fireAttackSound.isPlaying){
							fireAttackSound.clip = waterAttack;
							fireAttackSound.Play();
						}
					}
				} else {
					CreateParticle (prefabFire);
						if (!fireAttackSound.isPlaying){
							fireAttackSound.clip = fireAttack;
							fireAttackSound.Play();
					}
				}
				nextFireTime = Time.time + delayFire;
			}
		}
	}

	void CreateParticle (GameObject prefab) {
		GameObject go = Instantiate(prefab, this.transform.position - transform.forward  * 2, this.transform.rotation) as GameObject;
		go.transform.parent = this.transform;
		Destroy(go, 0.6f);
        StartCoroutine(Stop());
	}
    IEnumerator Stop()
    {
        yield return new WaitForSeconds(0.45f);
        animator.SetBool("Casting", false);
    }

	public bool canShoot() {
		if (Time.time < nextFireTime) { return false; }
        else { return true; }
	}
	
	void OnTriggerEnter(Collider hit) {
		if (hit.transform.CompareTag ("Torch")) {
			torch = hit.gameObject;
			isInRange = true;
		}
	}
	
	void OnTriggerExit(Collider hit) {
		if (hit.transform.CompareTag ("Torch")) {
			isInRange = false;
		}
	}
}