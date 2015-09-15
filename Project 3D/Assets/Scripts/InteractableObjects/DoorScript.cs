using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]

public class DoorScript : InteractableObjectMovement
{
    
    public List<Transform> pressurePlatesToOpenDoor = new List<Transform>();
    public List<Transform> torchesLitToOpenDoor = new List<Transform>();

    List<Transform> completed = new List<Transform>();

    private AudioSource audioDoor;

    private int previousState;


    void Start()
    {
        audioDoor = GetComponent<AudioSource>();
        maxDistance = 0.1f;
        previousState = state;
    }

    void Update()
    {
        pressurePlateChecks();
        torchesLitChecks(); 
		CheckRequirements ();
        ManageAudio();


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
		if(completed.Count == (pressurePlatesToOpenDoor.Count + torchesLitToOpenDoor.Count)) /*&& requiredTorchToBeLit.GetComponent<TorchScript>().isLit)*/
		{
			state = 2;
            
		}
		else {
            state = 1;
            
        }
        //Debug.Log("completed count - > " + completed.Count);
//        Debug.Log("State for --> " + gameObject.name + " in " + state);
	}

    void ManageAudio() {
        if (audioDoor != null && !audioDoor.isPlaying && previousState != state) {
            audioDoor.Play();
            previousState = state;  //set currentstate as previousstate to prevent soundloop.
        }
    }
}