using UnityEngine;
using System.Collections;

public class BetterCameraScript : MonoBehaviour {
//
//	[SerializeField]
//	GameObject viewPoint;
	[SerializeField]
	GameObject big;
	[SerializeField]
	GameObject small;
	// Use this for initialization
	Vector3 camPos = new Vector3(20,20,0);
	Vector3 camSpeed = Vector3.zero;

	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
	
		transform.position = Vector3.SmoothDamp (transform.position, 
		                                             (big.transform.position - small.transform.position)/ 2 + small.transform.position + camPos,
		                                             ref camSpeed ,0.5f);

		float distance = Vector3.Distance (big.transform.position, small.transform.position);
		float zoomValue = 0;
		if (distance > 10) {
			zoomValue = 10;
		} else {
			zoomValue = distance;
		}

		float fov = Camera.main.fieldOfView;
		Camera.main.fieldOfView = Mathf.Lerp(fov,30+zoomValue,Time.deltaTime * 2.5f);
//		Debug.Log (distance);
	}
}
