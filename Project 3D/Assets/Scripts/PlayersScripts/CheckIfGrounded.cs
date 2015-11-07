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
