using UnityEngine;
using System.Collections;

public class CameraControlScript : MonoBehaviour {
	
	Camera cam;
	public GameObject spawn;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        CamControl();
	}

	//check if player is outside screen pos
    void CamControl()
    {
        Vector3 screenCoord = cam.WorldToScreenPoint(this.transform.position);

        if (screenCoord.x < -20 || screenCoord.x > Screen.width + 20 || screenCoord.y < -20 || screenCoord.y > Screen.height + 20)
        {
            transform.position = spawn.transform.position;
        }
    }



}
