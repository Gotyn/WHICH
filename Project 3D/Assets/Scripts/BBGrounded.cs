using UnityEngine;
using System.Collections;

public class BBGrounded : CheckIfGrounded {

	void Update()
    {
        if (Grounded)
        {
            transform.parent.GetComponent<PlayerMovement>().enabled = true;
        }
        else
        {
            transform.parent.GetComponent<PlayerMovement>().enabled = false;
        }
    }
}
