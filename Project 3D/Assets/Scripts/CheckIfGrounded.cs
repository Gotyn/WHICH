using UnityEngine;
using System.Collections;

public class CheckIfGrounded : MonoBehaviour {

    [HideInInspector]
    public bool Grounded = true;

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();
    }

    void CheckGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 1f))
        {
            if (hit.transform.GetComponent<Collider>() == null) return;
            Grounded = true;
        }
        else
        {
            Grounded = false;

        }
        if (Grounded && GetComponentInChildren<FireAttackScript>().canShoot()) GetComponent<SmallBroMovement>().enabled = true;
    }
}
