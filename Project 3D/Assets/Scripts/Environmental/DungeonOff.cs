using UnityEngine;
using System.Collections;

public class DungeonOff : MonoBehaviour {

	bool s = false;
	bool b = false;
	
	GameObject light;
	
	// Use this for initialization
	void Start () {
		light = GameObject.Find ("Directional Light");
	}
	
	// Update is called once per frame
	void Update () {
		if (s && b) {
			RenderSettings.ambientLight = Color.white;
			light.SetActive(true);
		} 
	}
	
	void OnTriggerEnter(Collider hit)
	{
		if (hit.transform.CompareTag ("Big"))
			b = true;
		
		if (hit.transform.CompareTag ("Small"))
			s = true;
		
	}
}
