using UnityEngine;
using System.Collections;

public class BBGrounded : CheckIfGrounded {

    DeathScript deathScript;
    public bool kicking = false;

    void Start()
    {
        deathScript = transform.root.GetComponentInChildren<DeathScript>();

    }
	void Update()
    {
        CheckGrounded();
        if (Grounded && deathScript.respawned && !cutScene && !kicking)
        {
            transform.parent.GetComponent<PlayerMovement>().enabled = true;
        }
        else
        {
            transform.parent.GetComponent<PlayerMovement>().enabled = false;
        }




       // if(transform.parent.root.CompareTag("Big")) Debug.Log(Grounded);
    }

   
}
