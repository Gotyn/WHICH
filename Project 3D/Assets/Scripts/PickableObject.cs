using UnityEngine;
using System.Collections;

public class PickableObject : MonoBehaviour {

	Vector3 startPos;
	public bool pickable = false;
	Camera cam;

	void Start () {
		startPos = this.transform.position;
		cam = Camera.main;
	}

	void Update () {
		CamControl();
	}
	
	//check if player is outside screen pos
	void CamControl()
	{
		Vector3 screenCoord = cam.WorldToScreenPoint(this.transform.position);
		
		if (screenCoord.x < -20 || screenCoord.x > Screen.width + 20 || screenCoord.y < -20 || screenCoord.y > Screen.height + 20)
		{
			transform.position = startPos;
		}
	}
}
