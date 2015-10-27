﻿using UnityEngine;
using System.Collections;

public class DeathScript : MonoBehaviour {

    PlayerMovement playerMovement;
    Transform mainTranform;
    CameraControlScript camControl;
    [SerializeField]
    GameObject smokePrefab;
    [HideInInspector]
    public bool respawned = true;

    
	// Use this for initialization
	void Start () {

        camControl = GetComponentInParent<CameraControlScript>();
        playerMovement = GetComponentInParent<PlayerMovement>();
        mainTranform = transform.parent;
	}

    void OnTriggerEnter(Collider hit)
    {
        if (hit.CompareTag("Log"))
        {
            GameObject go = Instantiate(smokePrefab, transform.position, Quaternion.identity) as GameObject;
            Destroy(go, 1);
            //camControl.enabled = false;
            //mainTranform.position = new Vector3(1000, 0, 1000);
            respawned = false;
            playerMovement.enabled = false;
           // StartCoroutine(respawn());
            camControl.Respawn();
        }
    }

    IEnumerator respawn()
    {
        yield return new WaitForSeconds(1f);
        camControl.Respawn();
    }
}