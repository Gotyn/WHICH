using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to Holder (child of BigBro).
/// 
/// It takes care of checking and holding objects.
/// </summary>


public class HolderTest : MonoBehaviour {
	//Components
	[SerializeField]
	private Transform holder;
	private Transform objectToPick;
	private Transform magicGuy;

    private PlayerMovement bigBroMovement;
    private PlayerInputScript bigInput;

    private Rigidbody objectRigidBody;
    private Rigidbody rigidBody;
    private Rigidbody magicGuyRigidBody;

    private Collider hitCheck;

    //Variables
    [SerializeField]
    private float pickDistance = 1f;
    private float distanceToPick = 5.0f;

    private bool holdingObject = false;
    private bool holdingPlayer = false;
    private bool canPickUpSomething = false;

	void Start() {
        bigBroMovement = GetComponentInParent<PlayerMovement>();
        bigInput = GetComponentInParent<PlayerInputScript>();
        rigidBody = GetComponentInParent<Rigidbody>();
    }

	void Update () {
		ControlPickedPlayer ();
		SetCorrectSpeed (); //If player has picked an object he gets his speed decreased.
		SetCorrectPos (); //Allign picked object on player handle if object glitches.
		if(canPickUpSomething) PickUpObject (); //Picking up the object 
		ControlPickedObject (); // Pick / drop settings of object.

	}

    void ControlPickedPlayer() {
        if (holdingPlayer) {
            magicGuyRigidBody.velocity = rigidBody.velocity;
            
            if (Input.GetButtonDown(bigInput.interactControl_2) || DPadButtons.up) //throwing
            {
                magicGuyRigidBody.AddForce(transform.forward * 500f + transform.up * 250f);
                magicGuyRigidBody.useGravity = true;
                holdingPlayer = false;
            }
        } else {
            if (magicGuyRigidBody != null)
            {
                magicGuyRigidBody.useGravity = true;
                holdingPlayer = false;
            }
        }
    }

    void PickUpObject() {
        if (Input.GetButtonDown(bigInput.interactControl_1) || DPadButtons.down) {  
            if (hitCheck.gameObject.CompareTag("Small")) { //picking up player
                holdingPlayer = !holdingPlayer;
                magicGuy = hitCheck.transform;
                magicGuy.GetComponent<PlayerMovement>().enabled = false;
                magicGuyRigidBody = magicGuy.GetComponent<Rigidbody>();
                magicGuyRigidBody.useGravity = false;
                magicGuy.transform.position = holder.position;
                return;
            }

            if (hitCheck.gameObject.GetComponent("PickableObject") as PickableObject != null) {
                holdingObject = !holdingObject;
                objectToPick = hitCheck.transform;
                objectToPick.position = holder.position;
                objectToPick.rotation = holder.rotation;
                objectRigidBody = objectToPick.GetComponent<Rigidbody>();  
            }
        }
    }

	void ControlPickedObject() {
		if (holdingObject) {
			objectRigidBody.useGravity = false;
            objectRigidBody.velocity = rigidBody.velocity;
			objectRigidBody.constraints = RigidbodyConstraints.FreezePositionY;
			objectRigidBody.constraints = RigidbodyConstraints.FreezeRotation;
		} else {
			if(objectRigidBody != null)
			{
				objectRigidBody.useGravity = true;
                objectRigidBody.constraints = RigidbodyConstraints.None;
                objectRigidBody.constraints = RigidbodyConstraints.FreezeRotation;
                objectToPick = null;
			}
		}
	}

	void SetCorrectSpeed() {
        if (holdingObject || holdingPlayer) {
            bigBroMovement.speed = 5f;
		} else {
            bigBroMovement.speed = 10f;
		}
	}
	void SetCorrectPos() {
        if (holdingObject) {
            if (Vector3.Distance(objectToPick.transform.position, holder.position) > 1f) {
                objectToPick.transform.position = holder.position;
            }
        }

        if (holdingPlayer) {
            if (Vector3.Distance(magicGuy.position, holder.position) > 1f) {
                magicGuy.position = holder.position;
                magicGuy.rotation = holder.rotation;
            }
        }
	}

    void OnTriggerEnter(Collider hit) {
        if (hit.CompareTag("Small") || hit.gameObject.GetComponent("PickableObject") as PickableObject != null) {
            canPickUpSomething = true;
            hitCheck = hit;
        }
    }

    void OnTriggerExit(Collider hit) {
        if (!holdingObject && !holdingPlayer) {
            canPickUpSomething = false;
        }
    }
}
