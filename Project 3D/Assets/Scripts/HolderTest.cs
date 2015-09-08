using UnityEngine;
using System.Collections;

public class HolderTest : MonoBehaviour {

	//Components
	[SerializeField]
	private Transform holder;
	private Transform objectToPick;
	private Transform magicGuy;
	private BigBroMovement _bigBroMovement;
	private Rigidbody objectRigidBody;
    private Rigidbody rigidBody;
    private Rigidbody magicGuyRigidBody;

	//Variables
	float distanceToPick = 5.0f;
	bool holdingObject = false;
	bool holdingPlayer = false;
    [SerializeField]
    private float pickDistance = 1f;


	void Start(){
		_bigBroMovement = FindObjectOfType (typeof(BigBroMovement)) as BigBroMovement;
        rigidBody = GetComponent<Rigidbody>();
    }

	// Update is called once per frame
	void Update () {
		ControlPickedPlayer ();
		SetCorrectSpeed (); //If player has picked an object he gets his speed decreased.
		SetCorrectPos (); //Allign picked object on player handle if object glitches.
		PickUpObject (); //Picking up the object 
		ControlPickedObject (); // Pick / drop settings of object.

	}
    void ControlPickedPlayer()
    {
        if (holdingPlayer)
        {
            magicGuy.GetComponent<SmallBroMovement>().enabled = false;
            magicGuyRigidBody.velocity = rigidBody.velocity;
            magicGuyRigidBody.useGravity = false;
           // Debug.Log("Velocity Set. Holding a player ATM.");
            if (Input.GetButtonDown("Interact_Big_2")) //throwing
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

	void PickUpObject(){

        if (Input.GetButtonDown ("Interact_Big_1")) {  //if we are trying to pick an object 
			RaycastHit hit;
			if (Physics.Raycast (transform.position, transform.forward, out hit, pickDistance)) {

                if (hit.transform.gameObject.CompareTag("Small"))
                {
                    holdingPlayer = !holdingPlayer;

                    magicGuy = hit.transform;
                    magicGuyRigidBody = magicGuy.GetComponent<Rigidbody>();
                    magicGuy.transform.position = holder.position;
                    return;
                }

                if (hit.collider == null){ Debug.Log("Bla"); return;}
				if (!hit.transform.gameObject.GetComponent<PickableObject> ().pickable)return; //if object is pickable or not
				//Debug.Log("Not null --> " + hit.transform.gameObject.name + " <-- PICKED OBJECT");
				objectToPick = hit.transform; 
				objectRigidBody = objectToPick.GetComponent<Rigidbody> ();

                if (Vector3.Distance(transform.position, objectToPick.position) < distanceToPick)
                { //if we are actually in distance
                    
                  //  Debug.Log("----------->>>>>>>>" + hit.transform.name);

                    holdingObject = !holdingObject;
                    objectToPick.transform.position = holder.position;
                    objectToPick.transform.rotation = holder.rotation;


                }
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
}
