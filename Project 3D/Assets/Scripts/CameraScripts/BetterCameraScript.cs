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
    [HideInInspector]
    public Vector3 offset;
    [HideInInspector]
    public Vector3 newRotation;

	Vector3 camSpeed = Vector3.zero;
    Vector3 target;
    
    void Start () {
        //FOR START !!!!
        transform.rotation = Quaternion.Euler(45, 270, 0);
        offset = new Vector3(20, 20, 0); 
    }
	
	// Update is called once per frame
	void LateUpdate () {

        target = (big.transform.position - small.transform.position) / 2 + small.transform.position;
        transform.position = Vector3.SmoothDamp(transform.position,
                                                  target + offset,
                                                  ref camSpeed, 0.5f);

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
