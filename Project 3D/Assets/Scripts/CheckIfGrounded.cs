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

  

    #region Old On Grounded!
    /*
    void CheckGrounded()
    {
        rayDistance = transform.lossyScale.y + 0.1f;
        RaycastHit hit;

        Debug.DrawRay(transform.position + Vector3.forward * 0.48f, -transform.up * rayDistance, Color.red);
        Debug.DrawRay(transform.position - Vector3.forward * 0.48f, -transform.up * rayDistance, Color.red);
        Debug.DrawRay(transform.position + Vector3.left * 0.49f, -transform.up * rayDistance, Color.red);
        Debug.DrawRay(transform.position - Vector3.left * 0.49f, -transform.up * rayDistance, Color.red);

        if (Physics.Raycast(transform.position + Vector3.forward * 0.48f, -transform.up, out hit, rayDistance) ||
            Physics.Raycast(transform.position - Vector3.forward * 0.48f, -transform.up, out hit, rayDistance) ||
            Physics.Raycast(transform.position + Vector3.left * 0.48f, -transform.up, out hit, rayDistance) ||
            Physics.Raycast(transform.position - Vector3.left * 0.48f, -transform.up, out hit, rayDistance))
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
        //        Debug.Log(Grounded);

<<<<<<< HEAD

    }
    */
    #endregion

    void OnTriggerEnter(Collider hit)
    {
        if (hit.CompareTag("TestGround"))
            Grounded = true;
        
    }

    void OnTriggerExit(Collider hit)
    {
        if (hit.CompareTag("TestGround"))
        {
            Grounded = false;
        }
    }
}
