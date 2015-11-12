using UnityEngine;
using System.Collections;

public class HolderTest : MonoBehaviour
{
    enum GimbarSpeed {
        Box, Torch, Mithion
    }
    GimbarSpeed correctSpeed;
    //Components
    [SerializeField]
    Animator anim;
    Animator smallBroAnimator;
    [SerializeField]
    ParticleSystem mageGlow;
    [SerializeField]
    ParticleSystem mageGlowGreen;
    [SerializeField]
    private Transform holder;
    private Transform objectToPick;
    private Transform smallBro;
    private PlayerMovement bigBroMovement;
    private Rigidbody objectRigidBody;
    private Rigidbody rigidBody;
    private Rigidbody smallBroRigidBody;

    private PlayerInputScript bigInput;

    //Variables
    [HideInInspector]
    public bool holdingObject = false;
    [HideInInspector]
    public bool holdingPlayer = false;
    bool canPickUpSomething = false;
    Collider hitCheck;


    private bool counterGrab = false;
    [HideInInspector]
    public float elapsedTime;

	bool canTrow = false;

    void Start()
    {
        bigBroMovement = GetComponentInParent<PlayerMovement>();
        bigInput = GetComponentInParent<PlayerInputScript>();
        rigidBody = GetComponentInParent<Rigidbody>();
        smallBroAnimator = GameManagerScript.SB.GetComponentInChildren<Animator>();
        mageGlow.enableEmission = false;
        mageGlowGreen.enableEmission = false;
    }

    void Update()
    {
        if (!holdingObject && !holdingPlayer && !canPickUpSomething && (Input.GetButtonDown(bigInput.interactControl_1) || DPadButtons.down))
        {
            transform.parent.GetComponentInChildren<BBGrounded>().kicking = true;
            rigidBody.velocity = Vector3.zero;
            anim.SetBool("Kick", true);
            StartCoroutine(WaitKick());
        }

        SetCorrectSpeed(); //If player has picked an object he gets his speed decreased.

        SetCorrectPos(); //Allign picked object on player handle if object glitches.

        if (canPickUpSomething) PickUpObject(); //Picking up the object 

        ControlPickedObject(); // Pick / drop settings of object.

		ControlPickedPlayer(); // Function Okay ... else doesnt run all the time ANYMORE,
		// goes just once (so when we drop player manually , reset his "stuff");
    }

    void ControlPickedPlayer()
    {
        if (holdingPlayer)
        {
            if (!counterGrab) {  counterGrab = true; StartCoroutine(CounterGrab(5)); } //Mage gets dropped after few secs.
            elapsedTime += Time.deltaTime;

            mageGlow.enableEmission = false;
            mageGlowGreen.enableEmission = true;
            anim.SetBool("Carrying", true);
            smallBroAnimator.SetBool("Carried", true);
            smallBroRigidBody.velocity = rigidBody.velocity;

            if ((Input.GetButtonDown(bigInput.interactControl_1) || DPadButtons.down) && canTrow) //throwing
            {
                mageGlowGreen.enableEmission = false;
                anim.SetBool("Throw", true);
                smallBroAnimator.SetBool("Thrown", true);
                smallBroAnimator.SetBool("Carried", false);
                StartCoroutine(Wait());
                smallBroRigidBody.AddForce(transform.forward * 320f + transform.up * 160f);
                smallBroRigidBody.useGravity = true;
                holdingPlayer = false;
				canTrow = false;

            }
        }
        else if (smallBroRigidBody != null)
        {
            mageGlowGreen.enableEmission = false;
            if (!holdingObject) anim.SetBool("Carrying", false);
            smallBroAnimator.SetBool("Carried", false);
            smallBroRigidBody.useGravity = true;
            counterGrab = false;
            canTrow = false;
            holdingPlayer = false;
            smallBroRigidBody = null;
            smallBro = null;
        }

    }
    IEnumerator CounterGrab(float time)
    {
        elapsedTime = 0;
        while (holdingPlayer)
        {
            if(elapsedTime >= time) //dirty fix cuz while loop stops executing.
            {
                counterGrab = false;
                holdingPlayer = false;
				canTrow = false;
            }
            else yield return null;
        }
    }

    void PickUpObject()
    {
        if ((Input.GetButtonDown(bigInput.interactControl_1) || DPadButtons.down))
        {
            SetSpeedBasedOnObject(hitCheck);
            if (hitCheck.gameObject.CompareTag("Small") && !canTrow) //picking up player
            {
                if (holdingObject || !hitCheck.gameObject.GetComponentInChildren<SBGrounded>().Grounded) return; // if we really picked the player.
                Invoke("SetTrow", 0.1f);
                holdingPlayer = !holdingPlayer;
                smallBro = hitCheck.transform;
                smallBro.GetComponent<PlayerMovement>().enabled = false;
                smallBroRigidBody = smallBro.GetComponent<Rigidbody>();
                smallBroRigidBody.useGravity = false;
                smallBro.transform.position = holder.position;
                return;
            }
            else if (hitCheck.gameObject.GetComponent("PickableObject") as PickableObject != null)
            {
                if (holdingPlayer) return;

                holdingObject = !holdingObject;
                //---Transform
                objectToPick = hitCheck.transform;
                objectToPick.position = holder.position;
                objectToPick.rotation = holder.rotation;
                //---RigidBody
                objectRigidBody = objectToPick.GetComponent<Rigidbody>();
                objectRigidBody.useGravity = false;
                objectRigidBody.constraints = RigidbodyConstraints.FreezeRotation;
            }
        }
    }

	

    void SetSpeedBasedOnObject(Collider hitcheck)
    {
        if (hitCheck.CompareTag("Box")) correctSpeed = GimbarSpeed.Box;
        else if (hitCheck.CompareTag("Torch")) correctSpeed = GimbarSpeed.Torch;
        else if (hitCheck.CompareTag("Small")) correctSpeed = GimbarSpeed.Mithion;
    }

    void ControlPickedObject()
    {
        if (holdingObject)
        {
            anim.SetBool("Carrying", true);
            if (hitCheck.CompareTag("Box")) hitCheck.GetComponent<PickableObject>().Glow(false);
         
            objectRigidBody.velocity = rigidBody.velocity;
        //    objectRigidBody.constraints = RigidbodyConstraints.FreezePositionY;
           
        }
        else if (objectRigidBody != null)
        {
            if (!holdingPlayer) anim.SetBool("Carrying", false);
            objectRigidBody.velocity = Vector3.zero;
            objectRigidBody.useGravity = true;
            objectRigidBody.constraints = RigidbodyConstraints.None;
            objectRigidBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            objectToPick = null;
            objectRigidBody = null;
        }
    }

    void SetCorrectSpeed() {
        if (holdingObject || holdingPlayer)
        {
            switch (correctSpeed)
            {
                case GimbarSpeed.Mithion:
                    bigBroMovement.speed = 4f;
                    break;
                case GimbarSpeed.Box:
                    bigBroMovement.speed = 6f;
                    break;
                case GimbarSpeed.Torch:
                    bigBroMovement.speed = 5f;
                    break;
            }
        } else {
            bigBroMovement.speed = 5f;
        }
    }
    void SetCorrectPos()
    {
        if (holdingObject)
        {
            if (Vector3.Distance(objectToPick.transform.position, holder.position) > 0.8f)
            {
                objectToPick.transform.position = holder.position;
                objectToPick.transform.rotation = holder.rotation;
            }
        }
        if (holdingPlayer)
        {
            if (Vector3.Distance(smallBro.position, holder.position) > 1f)
            {
                smallBro.position = holder.position;
                smallBro.rotation = holder.rotation;
            }
        }
    }
    void OnTriggerEnter(Collider hit)
    {
        if (hit.CompareTag("Small") && !holdingObject)
        {
           // if (holdingObject) return; // this fixes when you go to mithion with a box , then you cant drop the box cuz the hitcheck gets reset.
           // same in on trigger stay.
            mageGlow.enableEmission = true;
            canPickUpSomething = true;
            hitCheck = hit;
        }
        else if (hit.gameObject.GetComponent("PickableObject") as PickableObject != null)
        {
            if (holdingObject) return; //dont send new info of other possible object ot pick if we are already holding one.
             
            if (hit.CompareTag("Box") && !holdingPlayer)
            {
                hit.GetComponent<PickableObject>().Glow(true);
            }
            canPickUpSomething = true;
            hitCheck = hit;

        }
        else if (hit.CompareTag("Handle") && !holdingPlayer && !holdingObject && !canPickUpSomething)
        {
            hit.GetComponent<HandleScript>().Glow(true);
            hit.GetComponent<HandleScript>().canHandle = true;
        }
    }
    // --------- On trigger entger maybe should be gone ?
    void OnTriggerStay(Collider hit)
    {
        if (hit.CompareTag("Small") && !holdingObject)
        {
            mageGlow.enableEmission = true;
            canPickUpSomething = true;
            hitCheck = hit;

        }
        else if (hit.gameObject.GetComponent("PickableObject") as PickableObject != null)
        {
            if (holdingObject) return; //dont send new info of other possible object ot pick if we are already holding one.

            if (hit.CompareTag("Box") && !holdingPlayer)
            {
                hit.GetComponent<PickableObject>().Glow(true);
            }
            canPickUpSomething = true;
            hitCheck = hit;

        }
    }

    void OnTriggerExit(Collider hit)
    {
        if (!holdingObject && !holdingPlayer)
        {
            canPickUpSomething = false;
        }
        if (hit.CompareTag("Box"))
        {
            hit.GetComponent<PickableObject>().Glow(false);
        }
        else if (hit.CompareTag("Handle"))
        {
            hit.GetComponent<HandleScript>().Glow(false);
            hit.GetComponent<HandleScript>().canHandle = false;
        }
        else if (hit.CompareTag("Small")) mageGlow.enableEmission = false;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("Throw", false);
        smallBroAnimator.SetBool("Thrown", false);
    }

    IEnumerator WaitKick()
    {
        yield return new WaitForSeconds(0.75f);
   
        anim.SetBool("Kick", false);
        transform.parent.GetComponentInChildren<BBGrounded>().kicking = false;
    }
    void SetTrow()
    {
        canTrow = true;
    }
}
