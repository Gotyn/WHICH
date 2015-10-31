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
	public Quaternion currentRotation;
    [HideInInspector]
    public Quaternion newRotation;

	Vector3 camSpeed = Vector3.zero;
    Vector3 target;

	public float startTime;
	public float length;
	float completed;
    public float myZoomValue;


    void Start () {
        //FOR START !!!!
		currentRotation = this.transform.rotation;
		newRotation = this.transform.rotation;
//		Debug.Log (currentRotation);
       //transform.rotation = currentRotation;
        offset = new Vector3(20, 20, 0); 
    }
	
	// Update is called once per frame
	void LateUpdate () {


		float distCovered = (Time.time - startTime) * 0.5f;
	//	Debug.Log (length);
		float completed = distCovered / length;
        //Debug.Log (completed);

        target = (big.transform.position + small.transform.position) / 2; //+ small.transform.position;
		transform.rotation = Quaternion.Lerp (currentRotation, newRotation, completed);
        transform.position = Vector3.SmoothDamp(transform.position,
                                                  target + offset,
                                                  ref camSpeed, 0.5f);

		if (completed > 1) {
			length = 0;
			currentRotation = newRotation;
		}

        float distance = Vector3.Distance (big.transform.position, small.transform.position);
        //if (distance > 20 && !blocked)
        //{
        //    bBlockWall = Instantiate(blockWall, big.transform.position + (big.transform.forward * 2), Quaternion.identity) as GameObject;
        //    sBlockWall = Instantiate(blockWall, small.transform.position + (small.transform.forward * 3), Quaternion.identity) as GameObject;
        //    Debug.Log("WALLS ADDED ");
        //    blocked = true;
        //}
        //else if (distance < 20 && blocked)
        //{
        //    Destroy(sBlockWall);
        //    Destroy(bBlockWall);
        //    Debug.Log("Walls Deleted");
        //    blocked = false;
        //}
        float zoomValue = 0;
		if (distance > myZoomValue) {
			zoomValue = myZoomValue;
		} else {
			zoomValue = distance;
		}

		float fov = Camera.main.fieldOfView;
		Camera.main.fieldOfView = Mathf.Lerp(fov,30+zoomValue,Time.deltaTime * 2.5f);
//		Debug.Log (distance);
	}
 
}
