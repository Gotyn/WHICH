using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BBGrounded : CheckIfGrounded {

    DeathScript deathScript;
    public bool kicking = false;
    List<bool> actions = new List<bool>();

    void Start()
    {
        deathScript = transform.root.GetComponentInChildren<DeathScript>();
        actions.Add(Grounded);
    }
	void Update()
    {
        Debug.Log("GIMBAR GR ->" + Grounded);
        if (Grounded && deathScript.respawned && !cutScene && !kicking)
        {
            transform.parent.GetComponent<PlayerMovement>().enabled = true;
        }
        else
        {
            transform.parent.GetComponent<PlayerMovement>().enabled = false;
        }
    }
}
