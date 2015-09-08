using UnityEngine;
using System.Collections;

public class CheckIfGrounded : MonoBehaviour {

    [HideInInspector]
    public bool Grounded = true;
	[HideInInspector]
	public bool grabbing = false;

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
		if (Grounded && GetComponentInChildren<FireAttackScript>().canShoot() && !GetComponentInChildren<FireAttackScript>().canRead && !grabbing ) GetComponent<PlayerMovement>().enabled = true;
    }
}
