using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to SmallBro (Wizard).
/// It is used to check and control whether the player is 
/// on the ground or not.
/// </summary>

public class CheckIfGrounded : MonoBehaviour {

    [HideInInspector]
    public bool Grounded = true;
	[HideInInspector]
	public bool grabbing = false;

    float rayDistance;

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

        if (Grounded && GetComponentInChildren<FireAttackScript>().canShoot() && !GetComponentInChildren<FireAttackScript>().canRead && !grabbing)
        {
            GetComponent<PlayerMovement>().enabled = true;           
        }
    }
}
