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

        Debug.DrawRay(transform.position, -transform.up * 0.85f, Color.red);
        if (Physics.Raycast(transform.position, -transform.up, out hit, 0.85f))
        {
            Debug.Log(hit.transform.name + " RAY OBJECT");
            if (hit.transform.CompareTag("TestGround"))
            {
                Debug.Log("GROUNDED");
                Grounded = true;
            }

        }
        else
        {
            Grounded = false;

        }


        if (Grounded && GetComponentInChildren<FireAttackScript>().canShoot() && !GetComponentInChildren<FireAttackScript>().canRead && !grabbing)
        {
            GetComponent<PlayerMovement>().enabled = true;
            Debug.Log("LALALALALALLALALALAL");
        }
    }
}
