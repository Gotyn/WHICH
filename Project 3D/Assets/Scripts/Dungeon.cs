﻿using UnityEngine;
using System.Collections;

public class Dungeon : MonoBehaviour {

	bool s = false;
	bool b = false;
	
	GameObject Lamppu;
	
	// Use this for initialization
	void Start () {
		Lamppu = GameObject.Find ("Directional Light");
	}
	
	// Update is called once per frame
	void Update () {
		if (s && b) {
			RenderSettings.ambientLight = Color.black;
			Lamppu.SetActive(false);
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
