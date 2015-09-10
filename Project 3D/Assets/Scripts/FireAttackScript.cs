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
    private PlayerMovement smallBroMovement;
    private CheckIfGrounded checkGrounded;
	
	float nextFireTime;
	float delayFire = 1.5f;

    //Variables
    [SerializeField]
    bool isInRange = false;

	public bool canCast = true;
	public bool canRead = false;

	void Start() {
		smallBroMovement = GetComponentInParent<PlayerMovement>();
        checkGrounded = GetComponentInParent<CheckIfGrounded>();
	}

    void Update () {
		DoFireAttack ();
	}

	void DoFireAttack () {
        if (canShoot () && checkGrounded.Grounded && canCast && !canRead) {
      
			if (Input.GetButtonDown ("SMALL_INTERACT_1")) {  //fire
				GetComponentInParent<Rigidbody> ().velocity = Vector3.zero;
				smallBroMovement.enabled = false;
				
				if (isInRange) {
					if (!torch.GetComponent<TorchScript> ().isLit) {
						CreateParticle (prefabFire);
						torch.GetComponent<TorchScript> ().SetFire ();
					} else {
						CreateParticle (prefabWater);
						torch.GetComponent<TorchScript> ().ExtinguishFire ();
					}
				} else {
					CreateParticle (prefabFire);
				}
				nextFireTime = Time.time + delayFire;
			}
		}
	}

	void CreateParticle (GameObject prefab) {
		GameObject go = Instantiate(prefab, this.transform.position - this.transform.forward * 1.5f, this.transform.rotation) as GameObject;
		go.transform.parent = this.transform;
		Destroy(go, 1.7f);
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