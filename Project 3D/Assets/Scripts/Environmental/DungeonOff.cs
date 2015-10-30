using UnityEngine;
using System.Collections;

public class DungeonOff : MonoBehaviour {

	bool s = false;
	bool b = false;
	
	GameObject myLight;
	
	// Use this for initialization
	void Start () {
		myLight = GameObject.Find ("Directional Light");
	}
	
	// Update is called once per frame
	void Update () {
		if (s && b) {
			Invoke("TurnOn",1);
		} 
	}

	void TurnOn () {
		RenderSettings.ambientLight = Color.white;
		myLight.SetActive(true);
	}
	void OnTriggerEnter(Collider hit)
	{
		if (hit.transform.CompareTag ("Big"))
			b = true;
		
		if (hit.transform.CompareTag ("Small"))
			s = true;
		
	}
}
