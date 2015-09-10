using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to SmallBro (Wizard).
/// It is used to check and control whether the player is 
/// on the ground or not.
/// </summary>

public class CheckIfGrounded : MonoBehaviour {
    FireAttackScript fireATS;
    DeathScript deathScript;

    [HideInInspector]
    public bool Grounded = true;

	[HideInInspector]
	public bool grabbing = false;
    float rayDistance;

    void Start()
    {
        fireATS = FindObjectOfType(typeof(FireAttackScript)) as FireAttackScript;
        deathScript = GetComponentInChildren<DeathScript>();
    }


    void Update()
    {
        CheckGrounded();
    }

    void CheckGrounded()
    {
        rayDistance = transform.lossyScale.y / 2 + 0.05f;
        RaycastHit hit;
        Debug.DrawRay(transform.position, -transform.up * rayDistance, Color.red);
        if (Physics.Raycast(transform.position, -transform.up, out hit, rayDistance))
        {
            if (hit.transform.CompareTag("TestGround"))
            {
                Grounded = true;
            }
            else
            {
                Grounded = false;
            }

        }
        else
        {
            Grounded = false;

        }
        Debug.Log(Grounded);

        if (Grounded && fireATS.canShoot() && !fireATS.canRead && !grabbing && deathScript.respawned)
        {
            GetComponent<PlayerMovement>().enabled = true;           
        }
    }
}
