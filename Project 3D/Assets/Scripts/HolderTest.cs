using UnityEngine;
using System.Collections;

public class HolderTest : MonoBehaviour {

	//Components
	[SerializeField]
	private Transform holder;
	private Transform objectToPick;
	private Transform magicGuy;
	private PlayerMovement _bigBroMovement;
	private Rigidbody objectRigidBody;
    private Rigidbody rigidBody;
    private Rigidbody magicGuyRigidBody;

    private PlayerInputScript bigInput;

	//Variables
	float distanceToPick = 5.0f;
	bool holdingObject = false;
	bool holdingPlayer = false;
    bool canPickUpSomething = false;
    Collider hitCheck;
    [SerializeField]
    private float pickDistance = 1f;


	void Start(){
		_bigBroMovement = FindObjectOfType (typeof(PlayerMovement)) as PlayerMovement;
        bigInput = GetComponentInParent<PlayerInputScript>();
        rigidBody = GetComponentInParent<Rigidbody>();
    }

	// Update is called once per frame
	void Update () {
		ControlPickedPlayer ();
		SetCorrectSpeed (); //If player has picked an object he gets his speed decreased.
		SetCorrectPos (); //Allign picked object on player handle if object glitches.
		if(canPickUpSomething) PickUpObject (); //Picking up the object 
		ControlPickedObject (); // Pick / drop settings of object.

	}
    void ControlPickedPlayer()
    {
        if (holdingPlayer)
        {
            magicGuy.GetComponent<PlayerMovement>().enabled = false;
            magicGuyRigidBody.velocity = rigidBody.velocity;
            magicGuyRigidBody.useGravity = false;
            // Debug.Log("Velocity Set. Holding a player ATM.");

            if (Input.GetButtonDown(bigInput.interactControl_2) || DPadButtons.up) //throwing
            {
                magicGuyRigidBody.AddForce(transform.forward * 500f + transform.up * 250f);
                magicGuyRigidBody.useGravity = true;
                holdingPlayer = false;
            }
        }
        else
        {
            if (magicGuyRigidBody != null)
            {
                magicGuyRigidBody.useGravity = true;
                holdingPlayer = false;
            }
        }

    }

    void PickUpObject()
    {
        if (Input.GetButtonDown(bigInput.interactControl_1) || DPadButtons.down)  
        {  
            if (hitCheck.gameObject.CompareTag("Small")) //picking up player
            {
                holdingPlayer = !holdingPlayer;
                magicGuy = hitCheck.transform;
                magicGuyRigidBody = magicGuy.GetComponent<Rigidbody>();
                magicGuy.transform.position = holder.position;
                return;
            }

            //Debug.Log("That wasnt the player.");
            //Debug.Log(hitCheck.gameObject.name + "<=== BLA BLA BLA");

            if (hitCheck.gameObject.GetComponent("PickableObject") as PickableObject != null)
            {
                holdingObject = !holdingObject;
                objectToPick = hitCheck.transform;
                objectToPick.position = holder.position;
                objectToPick.rotation = holder.rotation;
                objectRigidBody = objectToPick.GetComponent<Rigidbody>();  
            }
                        

        }
    }


	void ControlPickedObject(){
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
	void SetCorrectSpeed(){
		if (holdingObject || holdingPlayer) {
            _bigBroMovement.speed = 5f;
		} else {
            _bigBroMovement.speed = 10f;
		}
	}
	void SetCorrectPos(){
        if (holdingObject)
        {
            if (Vector3.Distance(objectToPick.transform.position, holder.position) > 1f)
            {
                objectToPick.transform.position = holder.position;
                Debug.Log("--- > Position Of Picked Object FIXED!");
            }
        }
        if (holdingPlayer)
        {
            if (Vector3.Distance(magicGuy.position, holder.position) > 1f)
            {
                magicGuy.position = holder.position;
                magicGuy.rotation = holder.rotation;
                Debug.Log("--- > Position Of Picked PLAYER FIXED!");
            }
        }
	}
    void OnTriggerEnter(Collider hit)
    {
        if (hit.CompareTag("Small") || hit.gameObject.GetComponent("PickableObject") as PickableObject != null)
        {
            Debug.Log("Enter");
            canPickUpSomething = true;
            hitCheck = hit;
        }
    }

    void OnTriggerExit(Collider hit)
    {
        if (!holdingObject && !holdingPlayer)
        {
            Debug.Log("Exit");
            canPickUpSomething = false;
        }
    }
}
