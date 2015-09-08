using UnityEngine;
using System.Collections;

public class FireAttackScript : MonoBehaviour {
	//Components
	[SerializeField]
	GameObject prefabFire;
	[SerializeField]
	GameObject prefabWater;
	[HideInInspector]
	public Transform torchTransform;
	GameObject torch;
	GameObject campfire;

    //SmallBro's movement
    private PlayerMovement smallBroMovement;
    private PlayerInputScript playerInput;

    private CheckIfGrounded checkGrounded;
	
	
	
	float nextFireTime;
	float delayFire = 1.5f;
	
	//Variables
	bool isInRange = false;
	bool isInRangeCamp = false;
	public bool canCast = true;
	public bool canRead = false;

	void Start()
	{
		smallBroMovement = GetComponentInParent<PlayerMovement>();
        playerInput = GetComponentInParent<PlayerInputScript>();
        checkGrounded = GetComponentInParent<CheckIfGrounded>();
	}
	// Update is called once per frame
	void Update () {
		DoFireAttack ();
	}

	void DoFireAttack () {
		if (canShoot () && checkGrounded.Grounded && canCast && !canRead) {
			
			if (Input.GetButtonDown (playerInput.interactControl_1)) {  //fire
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
				} else if (isInRangeCamp) {
					if (!campfire.GetComponent<FirePlaceScript> ().isLit) {
						CreateParticle (prefabFire);
						campfire.GetComponent<FirePlaceScript> ().SetFire ();
					} else {
						CreateParticle (prefabWater);
						campfire.GetComponent<FirePlaceScript> ().ExtinguishFire ();
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

	public bool canShoot()
	{
		if (Time.time < nextFireTime)
		{
			return false;
		} else {
			return true;
		}
	}
	
	void OnTriggerEnter(Collider hit) {
		if (hit.transform.CompareTag ("Torch")) {
			torch = hit.gameObject;
			isInRange = true;
		}
		if (hit.transform.CompareTag ("CampFire")) {
			campfire = hit.gameObject;
			isInRangeCamp = true;
		}
	}
	
	void OnTriggerExit(Collider hit) {
		if (hit.transform.CompareTag ("Torch")) {
			isInRange = false;
		}
		if (hit.transform.CompareTag ("CampFire")) {
			isInRangeCamp = false;
		}

	}
}