using UnityEngine;
using System.Collections;

public class HolderTest : MonoBehaviour
{
   

    //Components
    [SerializeField]
    Animator anim;
    Animator smallAnim;
    [SerializeField]
    ParticleSystem mageGlow;
    [SerializeField]
    ParticleSystem mageGlowGreen;
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
    [HideInInspector]
    public bool holdingObject = false;
    [HideInInspector]
    public bool holdingPlayer = false;
    bool canPickUpSomething = false;
    Collider hitCheck;
    [SerializeField]
    private float pickDistance = 1f;

    private bool counterGrab = false;
    float elapsedTime;

    void Start()
    {
        _bigBroMovement = GetComponentInParent<PlayerMovement>();
        bigInput = GetComponentInParent<PlayerInputScript>();
        rigidBody = GetComponentInParent<Rigidbody>();
        smallAnim = GameObject.FindGameObjectWithTag("Small").GetComponentInChildren<Animator>();
        mageGlow.enableEmission = false;
        mageGlowGreen.enableEmission = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!holdingObject && !holdingPlayer && !canPickUpSomething && (Input.GetButtonDown(bigInput.interactControl_1) || DPadButtons.down))
        {
            transform.parent.GetComponentInChildren<BBGrounded>().kicking = true;
            rigidBody.velocity = Vector3.zero;
      //      Debug.Log("Kick " + rigidBody.velocity + " ---" + transform.root.gameObject.name);
            anim.SetBool("Kick", true);
            StartCoroutine(WaitKick());
        }
       
        ControlPickedPlayer(); // Function Okay ... else doesnt run all the time ANYMORE,
                               // goes just once (so when we drop player manually , reset his "stuff");

        SetCorrectSpeed(); //If player has picked an object he gets his speed decreased.

        SetCorrectPos(); //Allign picked object on player handle if object glitches.

        if (canPickUpSomething) PickUpObject(); //Picking up the object 

        ControlPickedObject(); // Pick / drop settings of object.



        //RaycastHit hit;
        //Debug.DrawRay(transform.position, transform.forward * 0.5f);
        //if (Physics.Raycast(transform.position, transform.forward, out hit, 0.5f))
        //{
        //    rigidBody.velocity = Vector3.zero;
        //}

    }
    void ControlPickedPlayer()
    {
      //  Debug.Log("I am being called allthe fucking time.");
        if (holdingPlayer)
        {
       
            Debug.Log(elapsedTime);
            if (!counterGrab) { Debug.Log("Countergrab true,resetting time, starting routing."); counterGrab = true; StartCoroutine(CounterGrab(5)); } //Mage gets dropped after few secs.
            elapsedTime += Time.deltaTime;

            mageGlow.enableEmission = false;
            mageGlowGreen.enableEmission = true;
            anim.SetBool("Carrying", true);
            smallAnim.SetBool("Carried", true);
            magicGuyRigidBody.velocity = rigidBody.velocity;
            // Debug.Log("Velocity Set. Holding a player ATM.");

            if (Input.GetButtonDown(bigInput.interactControl_2) || DPadButtons.up) //throwing
            {
                mageGlowGreen.enableEmission = false;
                anim.SetBool("Throw", true);
                smallAnim.SetBool("Thrown", true);
                smallAnim.SetBool("Carried", false);
                StartCoroutine(Wait());
                magicGuyRigidBody.AddForce(transform.forward * 320f + transform.up * 160f);
                magicGuyRigidBody.useGravity = true;
                holdingPlayer = false;

            }
        }
        else if (magicGuyRigidBody != null)
        {
            mageGlowGreen.enableEmission = false;
            if (!holdingObject) anim.SetBool("Carrying", false);
            smallAnim.SetBool("Carried", false);
            magicGuyRigidBody.useGravity = true;
            counterGrab = false;
            holdingPlayer = false;
            magicGuyRigidBody = null;
            magicGuy = null;
        }

    }
    IEnumerator CounterGrab(float time)
    {
        Debug.Log("STARTED A COROUTINE !");

        elapsedTime = 0;
        while (holdingPlayer)
        {
            Debug.Log("-");
            if(elapsedTime >= time) //dirty fix cuz while loop stops executing.
            {
                Debug.Log("DROP !!!!");
                counterGrab = false;
                holdingPlayer = false;
            }
            else yield return null;
        }
        Debug.Log("finished");
       
    }
    void PickUpObject()
    {
        if (Input.GetButtonDown(bigInput.interactControl_1) || DPadButtons.down)
        {

            if (hitCheck.gameObject.CompareTag("Small")) //picking up player
            {
                if (holdingObject) return; // Just making sure we cant pick player while having object in hand.
                holdingPlayer = !holdingPlayer;
                //     Debug.Log(holdingPlayer + "Hold");
                magicGuy = hitCheck.transform;
                magicGuy.GetComponent<PlayerMovement>().enabled = false;
                magicGuyRigidBody = magicGuy.GetComponent<Rigidbody>();
                magicGuyRigidBody.useGravity = false;
                magicGuy.transform.position = holder.position;
                return;
            }
            else if (hitCheck.gameObject.GetComponent("PickableObject") as PickableObject != null)
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
   

    void ControlPickedObject()
    {

        if (holdingObject)
        {
            anim.SetBool("Carrying", true);
            if (hitCheck.CompareTag("Box")) hitCheck.GetComponent<PickableObject>().Glow(false);
            objectRigidBody.useGravity = false;
            objectRigidBody.velocity = rigidBody.velocity;
            objectRigidBody.constraints = RigidbodyConstraints.FreezePositionY;
            objectRigidBody.constraints = RigidbodyConstraints.FreezeRotation;
        }
        else if (objectRigidBody != null)
        {

            if (!holdingPlayer) anim.SetBool("Carrying", false);
            objectRigidBody.useGravity = true;
            objectRigidBody.constraints = RigidbodyConstraints.None;
            objectRigidBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            objectToPick = null;
            objectRigidBody = null;
            //Debug.Log("CALLED !");
        }

    }
    void SetCorrectSpeed()
    {
        if (holdingObject || holdingPlayer)
        {
            _bigBroMovement.speed = 4f;
        }
        else
        {
            _bigBroMovement.speed = 5f;
        }
    }
    void SetCorrectPos()
    {
        if (holdingObject)
        {
            if (Vector3.Distance(objectToPick.transform.position, holder.position) > 1f)
            {
                objectToPick.transform.position = holder.position;
                //  Debug.Log("--- > Position Of Picked Object FIXED!");
            }
        }
        if (holdingPlayer)
        {
            if (Vector3.Distance(magicGuy.position, holder.position) > 1f)
            {
                magicGuy.position = holder.position;
                magicGuy.rotation = holder.rotation;
                //   Debug.Log("--- > Position Of Picked PLAYER FIXED!");
            }
        }
    }
    void OnTriggerEnter(Collider hit)
    {

        if (hit.CompareTag("Small"))
        {
            mageGlow.enableEmission = true;
            canPickUpSomething = true;
            hitCheck = hit;
        }
        else if (hit.gameObject.GetComponent("PickableObject") as PickableObject != null)
        {
            if (hit.CompareTag("Box") && !holdingPlayer)
            {
                hit.GetComponent<PickableObject>().Glow(true);
            }
            canPickUpSomething = true;
            hitCheck = hit;
        }
        else if (hit.CompareTag("Handle") && !holdingPlayer && !holdingObject)
        {
            
            hit.GetComponent<HandleScript>().Glow(true);
            hit.GetComponent<HandleScript>().canHandle = true;
        }

    }

    void OnTriggerStay(Collider hit)
    {
        if (hit.CompareTag("Small"))
        {
            mageGlow.enableEmission = true;
            canPickUpSomething = true;
            hitCheck = hit;
        }
    }
    void OnTriggerExit(Collider hit)
    {
        if (!holdingObject && !holdingPlayer)
        {
            if (hit.CompareTag("Box"))
            {
                hit.GetComponent<PickableObject>().Glow(false);
            }

            if (hit.CompareTag("Handle"))
            {
                hit.GetComponent<HandleScript>().Glow(false);
                hit.GetComponent<HandleScript>().canHandle = false;
            }
            //  Debug.Log("Exit");
            canPickUpSomething = false;
        }
        if (hit.CompareTag("Small")) mageGlow.enableEmission = false;
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("Throw", false);
        smallAnim.SetBool("Thrown", false);
    }

    IEnumerator WaitKick()
    {
        yield return new WaitForSeconds(0.75f);
   
        anim.SetBool("Kick", false);
        transform.parent.GetComponentInChildren<BBGrounded>().kicking = false;
    }
}
