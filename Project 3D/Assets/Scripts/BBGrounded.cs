using UnityEngine;
using System.Collections;

public class BBGrounded : CheckIfGrounded {
    DeathScript deathScript;

    void Start()
    {
        deathScript = transform.root.GetComponentInChildren<DeathScript>();
    }
	void Update()
    {
        if (Grounded && deathScript.respawned)
        {
            transform.parent.GetComponent<PlayerMovement>().enabled = true;
        }
        else
        {
            transform.parent.GetComponent<PlayerMovement>().enabled = false;
        }
    }
}
