using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExpertDoorScript : DoorScript {

	override public void CheckRequirements () {
		if(completed.Count == 1) 
		{
			state = 2;
			
		}
		else {
			state = 1;
			
		}
	}
}
