using UnityEngine;
using System.Collections;

public class ActivateElevator : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	void OnTriggerEnter(Collider hit)
    {
        if (hit.transform.gameObject.CompareTag("Small"))
        {
            GameObject.Find("Elevator").GetComponent<Animation>().Play();
            Destroy(this, 0.5f);
        }
    }

}
