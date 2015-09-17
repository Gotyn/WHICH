﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExpertDoorScript : InteractableObjectMovement {

	
	public List<Transform> pressurePlatesToOpenDoor = new List<Transform>();
	public List<Transform> torchesLitToOpenDoor = new List<Transform>();

	List<Transform> completed = new List<Transform>();


	private int previousState;
	// Use this for initialization
	void Start () {
		maxDistance = 0.1f;
		previousState = state;
	}
	
	// Update is called once per frame
	void Update () {
		pressurePlateChecks();
		torchesLitChecks(); 
		CheckRequirements ();
	}

	void torchesLitChecks()
	{
		for (int i = 0; i < torchesLitToOpenDoor.Count; i++)
		{
			Transform current = torchesLitToOpenDoor[i];
			if (current.GetComponent<TorchScript>().isLit == (current.GetComponent<TorchScript>().needActivated))
			{
				//  Debug.Log("Completed true! for -> " + i.ToString());
				current.GetComponent<TorchScript>().completed = true;
				SortList(false, current);
			}
			else
			{
				current.GetComponent<TorchScript>().completed = false;
				SortList(true, current);
			}
		}
	}
	void pressurePlateChecks()
	{
		for (int i = 0; i < pressurePlatesToOpenDoor.Count; i++)
		{
			Transform current = pressurePlatesToOpenDoor[i];
			if (current.GetComponentInChildren<PressurePlateScript>().activated == (current.GetComponentInChildren<PressurePlateScript>().needActivated))
			{
				
				// Debug.Log("Completed true! for -> " + i.ToString());
				current.GetComponentInChildren<PressurePlateScript>().completed = true;
				SortList(false, current);
				
			}
			else
			{
				current.GetComponentInChildren<PressurePlateScript>().completed = false;
				SortList(true, current);
			}
		}
	}
	

	
	void SortList(bool remove, Transform item)
	{
		if (!remove)
		{
			if (!completed.Contains(item))
			{
				completed.Add(item);
			}
		}
		else
		{
			if (completed.Contains(item))
			{
				completed.Remove(item);
			}
		}
	}
	
	void CheckRequirements () {
//		Debug.Log (completed.Count);
		if(completed.Count == 1) /*&& requiredTorchToBeLit.GetComponent<TorchScript>().isLit)*/
		{
			state = 2;
			
		}
		else {
			state = 1;
			
		}
	}
}