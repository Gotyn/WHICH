using UnityEngine;
using System.Collections;

public class SpikesMovement : InteractableObjectMovement
{
    [SerializeField]
    Transform requiredPressurePlate;

    void Start()
    {
        maxDistance = 0.1f;
        if (requiredPressurePlate == null) Debug.Log("Required pressure plate to open door needs to be dragged into inspector!");
    }

    void Update()
    {
		CheckRequirements ();
    }

	void CheckRequirements () {
		if (requiredPressurePlate.GetComponentInChildren<PressurePlateScript>().activated)
		{
			state = 2;
		}
		else { state = 1; }
	}

}