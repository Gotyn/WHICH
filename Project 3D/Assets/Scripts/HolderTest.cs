using UnityEngine;
using System.Collections;

public class HolderTest : MonoBehaviour {

    //Components
    [SerializeField]
    Animator anim;
    Animator smallAnim;

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
        _bigBroMovement = GetComponentInParent<PlayerMovement>();
        bigInput = GetComponentInParent<PlayerInputScript>();
        rigidBody = GetComponentInParent<Rigidbody>();
        smallAnim = GameObject.FindGameObjectWithTag("Small").GetComponentInChildren<Animator>();

    }

	// Update is called once per frame
	void Update () {
		ControlPickedPlayer ();
		SetCorrectSpeed (); //If player has picked an object he gets his speed decreased.
		SetCorrectPos (); //Allign picked object on player handle if object glitches.
		if(canPickUpSomething) PickUpObject (); //Picking up the object 
		ControlPickedObject (); // Pick / drop settings of object.
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * 0.5f);
        if (Physics.Raycast(transform.position, transform.forward, out hit, 0.5f))
        {
            rigidBody.velocity = Vector3.zero;
        }

	}
    void ControlPickedPlayer()
    {
        if (holdingPlayer)
        {
            anim.SetBool("Carrying", true);
            smallAnim.SetBool("Carried", true);
            magicGuyRigidBody.velocity = rigidBody.velocity;
            // Debug.Log("Velocity Set. Holding a player ATM.");

            if (Input.GetButtonDown(bigInput.interactControl_2) || DPadButtons.up) //throwing
            {
                anim.SetBool("Throw", true);
                smallAnim.SetBool("Thrown", true);
                smallAnim.SetBool("Carried", false);
                StartCoroutine(Wait());
                magicGuyRigidBody.AddForce(transform.forward * 300f + transform.up * 150f);
                magicGuyRigidBody.useGravity = true;
                holdingPlayer = false;
            }
        }
        else
        {
            if (magicGuyRigidBody != null)
            {
                if (!holdingObject) anim.SetBool("Carrying", false);
                smallAnim.SetBool("Carried", false);
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
                if (holdingObject) return;
                holdingPlayer = !holdingPlayer;
                Debug.Log(holdingPlayer + "Hold");
                magicGuy = hitCheck.transform;
                magicGuy.GetComponent<PlayerMovement>().enabled = false;
                magicGuyRigidBody = magicGuy.GetComponent<Rigidbody>();
                magicGuyRigidBody.useGravity = false;
                magicGuy.transform.position = holder.position;
                return;
            }

            //Debug.Log("That wasnt the player.");
            //Debug.Log(hitCheck.gameObject.name + "<=== BLA BLA BLA");

            if (hitCheck.gameObject.GetComponent("PickableObject") as PickableObject != null)
            {
                if (holdingPlayer) return;
                holdingObject = !holdingObject;
                objectToPick = hitCheck.transform;
                objectToPick.position = holder.position;
                objectToPick.rotation = holder.rotation;
                objectRigidBody = objectToPick.GetComponent<Rigidbody>();
            }
		}
	}
	IEnumerator Wait()
	{
		yield return new WaitForSeconds(0.1f);
		anim.SetBool("Throw", false);
		smallAnim.SetBool("Thrown", false);
    }
	void ControlPickedObject(){
		if (holdingObject) {
            anim.SetBool("Carrying", true);
			if (hitCheck.CompareTag("Box"))hitCheck.GetComponent<PickableObject>().Glow(false);
			objectRigidBody.useGravity = false;
            objectRigidBody.velocity = rigidBody.velocity;
			objectRigidBody.constraints = RigidbodyConstraints.FreezePositionY;
			objectRigidBody.constraints = RigidbodyConstraints.FreezeRotation;
		} else {
			if(objectRigidBody != null)
			{
                if(!holdingPlayer) anim.SetBool("Carrying", false);
                objectRigidBody.useGravity = true;
                objectRigidBody.constraints = RigidbodyConstraints.None;                
				objectRigidBody.constraints = RigidbodyConstraints.FreezePositionX |RigidbodyConstraints.FreezePositionZ|RigidbodyConstraints.FreezeRotation;
                objectToPick = null;
			}
		}
	}
	void SetCorrectSpeed(){        
        if (holdingObject || holdingPlayer) {
            _bigBroMovement.speed = 4f;
		} else {
            _bigBroMovement.speed = 5f;
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
			if (hit.CompareTag("Box") && !holdingPlayer) {
				hit.GetComponent<PickableObject>().Glow(true);
			}
         //   Debug.Log("Enter");
            canPickUpSomething = true;
            hitCheck = hit;
        }
		
		if (hit.CompareTag("Handle") && !holdingPlayer) {
			Debug.Log("InRange");
			hit.GetComponent<HandleScript>().Glow(true);
			hit.GetComponent<HandleScript>().canHandle = true;
		}

    }
	

    void OnTriggerExit(Collider hit)
    {
        if (!holdingObject && !holdingPlayer)
        {
			if (hit.CompareTag("Box")) {
				hit.GetComponent<PickableObject>().Glow(false);
			}

			if (hit.CompareTag("Handle")) {
				hit.GetComponent<HandleScript>().Glow(false);
				hit.GetComponent<HandleScript>().canHandle = false;
			}
          //  Debug.Log("Exit");
            canPickUpSomething = false;
        }

    }
}
