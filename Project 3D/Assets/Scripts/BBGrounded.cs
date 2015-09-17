using UnityEngine;
using System.Collections;

public class BBGrounded : CheckIfGrounded {

    DeathScript deathScript;
    //float rayDistance;

    void Start()
    {
        deathScript = transform.root.GetComponentInChildren<DeathScript>();
    }
	void Update()
    {
        CheckGrounded();
        if (Grounded && deathScript.respawned && !cutScene)
        {
            transform.parent.GetComponent<PlayerMovement>().enabled = true;
        }
        else
        {
            transform.parent.GetComponent<PlayerMovement>().enabled = false;
        }



//        Debug.Log("BB -- Cutscene: " + cutScene);
    }

   
}
