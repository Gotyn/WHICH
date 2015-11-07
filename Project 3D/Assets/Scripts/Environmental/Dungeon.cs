using UnityEngine;
using System.Collections;

public class Dungeon : MonoBehaviour {

	bool SmallBroInPos = false;
	bool BigBroInPos = false;
	
	GameObject DirLight;
    
    // Use this for initialization
    void Start () {
		DirLight = GameObject.Find ("Directional Light");
	}
	
	// Update is called once per frame
	void Update () {
		if (SmallBroInPos && BigBroInPos) {
			RenderSettings.ambientLight = Color.black;
			DirLight.SetActive(false);
		} 
	}
	
	void OnTriggerEnter(Collider hit)
	{
		if (hit.transform.CompareTag ("Big"))
			BigBroInPos = true;
		
		if (hit.transform.CompareTag ("Small"))
			SmallBroInPos = true;
		
	}

	void OnTriggerExit(Collider hit)
	{
		if (hit.transform.CompareTag ("Big"))
			BigBroInPos = false;
		
		if (hit.transform.CompareTag ("Small"))
			SmallBroInPos = false;
		
	}

}
