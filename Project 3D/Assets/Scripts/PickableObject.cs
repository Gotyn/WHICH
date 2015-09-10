using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to each object that needs to be
/// pickupable.
/// </summary>

public class PickableObject : MonoBehaviour {
	private Vector3 startPos;
    private Camera cam;
    public bool pickable = false;

	void Start () {
		startPos = this.transform.position;
		cam = Camera.main;
	}

	void Update () {
		CamControl();
	}
	
	//check if player is outside screen pos
	void CamControl() { 
		Vector3 screenCoord = cam.WorldToScreenPoint(this.transform.position);
		
		if (screenCoord.x < -20 || screenCoord.x > Screen.width + 20 || screenCoord.y < -20 || screenCoord.y > Screen.height + 20) {
			transform.position = startPos;
		}
	}
}
