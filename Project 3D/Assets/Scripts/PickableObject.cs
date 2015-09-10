using UnityEngine;
using System.Collections;

public class PickableObject : MonoBehaviour {

	Vector3 startPos;
	public bool pickable = false;
	Camera cam;
	ParticleSystem glow;

	void Start () {
	
		startPos = this.transform.position;
		cam = Camera.main;
		glow = GetComponentInChildren<ParticleSystem> ();
		glow.gameObject.SetActive (false);
	}

	void Update () {
		CamControl();
	

	}

	public void Glow(bool on){
		if (on) {
			glow.gameObject.SetActive (true);
		} else {
			glow.gameObject.SetActive (false);
		}
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
