using UnityEngine;
using System.Collections;

public class SBGrounded : CheckIfGrounded {

    FireAttackScript fireATS;
    DeathScript deathScript;

    [HideInInspector]
    public bool grabbing = false;

    void Start()
    {
        fireATS = FindObjectOfType(typeof(FireAttackScript)) as FireAttackScript;
        deathScript = transform.root.GetComponentInChildren<DeathScript>();
    }


    void Update()
    {
        if (Grounded && fireATS.canShoot() && !fireATS.canRead && !grabbing && deathScript.respawned && !cutScene)
        {
            transform.parent.GetComponent<PlayerMovement>().enabled = true;
        }
        else
        {
            transform.parent.GetComponent<PlayerMovement>().enabled = false;
        }
        Debug.Log("SB -- Cutscene: " + cutScene);
    }
}
