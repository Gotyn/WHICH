using UnityEngine;
using System.Collections;

public class LogEncounter : MonoBehaviour {

    bool eventStarted = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnTriggerEnter(Collider hit)
    {
        if(hit.CompareTag("Small") || hit.CompareTag("Big"))
        {
            if (!eventStarted) StartCoroutine(logEncounter());

        }
    }

    IEnumerator logEncounter()
    {
        yield return new WaitForSeconds(0.5f);

    }

    void spawnLog()
    {

    }
}
