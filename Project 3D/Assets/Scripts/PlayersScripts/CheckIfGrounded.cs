using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to SmallBro (Wizard).
/// It is used to check and control whether the player is 
/// on the ground or not.
/// </summary>

public class CheckIfGrounded : MonoBehaviour
{
    [HideInInspector]
    public bool Grounded = true;
    float rayDistance;

    [HideInInspector]
    public bool cutScene = false;


    

    public void CheckGrounded()
    {

        //RaycastHit hit;
        //rayDistance = transform.lossyScale.y / 2 + 0.08f;
        //Debug.DrawRay(transform.position + transform.forward * 0.4f, -transform.up * rayDistance, Color.red);
        //Debug.DrawRay(transform.position - transform.forward * 0.4f, -transform.up * rayDistance, Color.red);
        //Debug.DrawRay(transform.position + transform.right * 0.4f, -transform.up * rayDistance, Color.red);
        //Debug.DrawRay(transform.position - transform.right * 0.4f, -transform.up * rayDistance, Color.red);

        //if (Physics.Raycast(transform.position + transform.forward * 0.2f, -transform.up, out hit, rayDistance) ||
        //    Physics.Raycast(transform.position - transform.forward * 0.2f, -transform.up, out hit, rayDistance) ||
        //    Physics.Raycast(transform.position + transform.right * 0.2f, -transform.up, out hit, rayDistance) ||
        //    Physics.Raycast(transform.position - transform.right * 0.2f, -transform.up, out hit, rayDistance))
        //{
        //    if (hit.transform.CompareTag("TestGround"))
        //    {
        //        Grounded = true;
        //    }
        //}


    }


    void OnTriggerEnter(Collider hit)
    {
        if (hit.CompareTag("TestGround"))
        {
            Grounded = true;
        }
    }

    void OnTriggerExit(Collider hit)
    {
        if (hit.CompareTag("TestGround"))
        {
            Grounded = false;
        }
    }

    void OnTriggerStay(Collider hit)
    {
        if (hit.CompareTag("TestGround"))
        {
            Grounded = true;
        }
    }


}
