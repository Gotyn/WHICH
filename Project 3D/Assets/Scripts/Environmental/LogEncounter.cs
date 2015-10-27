﻿using UnityEngine;
using System.Collections;

public class LogEncounter : MonoBehaviour {
    [SerializeField]
    Transform startPos;
    [SerializeField]
    GameObject log;

    bool eventStarted = false;

    
	
	


    void OnTriggerEnter(Collider hit)
    {
        if(hit.CompareTag("Small") || hit.CompareTag("Big"))
        {
            if (!eventStarted)
            {
                eventStarted = true;
                StartCoroutine(logEncounter());
              
            }
        }
        if (hit.CompareTag("Log"))
        {
            Destroy(hit.gameObject);
        }
    }

    IEnumerator logEncounter()
    {
        while (eventStarted)
        {
            spawnLog();
            yield return new WaitForSeconds(4.5f);
            spawnLog();
            yield return new WaitForSeconds(4.5f);
            
        }
    }

    void spawnLog()
    {
        GameObject go = Instantiate(log, startPos.position, Quaternion.identity) as GameObject;
        go.GetComponent<Rigidbody>().AddForce(startPos.forward * 25, ForceMode.Impulse);
        /*if (go)*/ Destroy(go, 5f);
    }
}