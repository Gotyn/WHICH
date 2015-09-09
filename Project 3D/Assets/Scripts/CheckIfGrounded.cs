using UnityEngine;
using System.Collections;

public class CheckIfGrounded : MonoBehaviour {

    [HideInInspector]
    public bool Grounded = true;
	[HideInInspector]
	public bool grabbing = false;

    float rayDistance;

    // Update is called once per frame
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
