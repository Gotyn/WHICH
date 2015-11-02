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
    [HideInInspector] 
    public Vector3 lastPosBeforeShake; //Used to produce camera shake .. 
    GameObject bBlockWall, sBlockWall;
    public GameObject blockWall;
	public float startTime;
	public float length;
	float completed;
    public float myZoomValue;
    
    public float shake = 0f;
    public float shakeAmount = 75f;
    public float decreaseFactor = 1.0f;
    bool shaking = false;
    void Start () {
        //FOR START !!!!
		currentRotation = this.transform.rotation;
		newRotation = this.transform.rotation;
        offset = new Vector3(20, 20, 0); 
    }

	void ApplyPosition()
    {
        if (shake > 0) //if shaking do camera shake (move inside unit sphere).
        {
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, 
                                        lastPosBeforeShake + Random.insideUnitSphere * shakeAmount,
                                       ref camSpeed,0.1f);
            shake -= Time.deltaTime * decreaseFactor;
        }
        else
        {    //if not shaking apply normal centered position 
            shake = 0f;
            transform.position = Vector3.SmoothDamp(transform.position,
                                      target + offset,
                                      ref camSpeed, 0.5f);
        }
    }
    // Update is called once per frame
    void LateUpdate() {
      

		float distCovered = (Time.time - startTime) * 0.5f;
	//	Debug.Log (length);
		float completed = distCovered / length;
        //Debug.Log (completed);

        target = (big.transform.position + small.transform.position) / 2; //+ small.transform.position;
		transform.rotation = Quaternion.Lerp (currentRotation, newRotation, completed);

		if (completed > 1) {
			length = 0;
			currentRotation = newRotation;
		}

        float distance = Vector3.Distance (big.transform.position, small.transform.position);
        ApplyPosition();

        float zoomValue = 0;
		if (distance > myZoomValue) {
			zoomValue = myZoomValue;
		} else {
			zoomValue = distance;
		}

		float fov = Camera.main.fieldOfView;
		Camera.main.fieldOfView = Mathf.Lerp(fov,30+zoomValue,Time.deltaTime * 2.5f);
        //		Debug.Log (distance);
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Debug.Log("SHAKE IT REAL GOOD !");
        //    lastPosBeforeShake = transform.localPosition;
        //    shake = 1;
           
        //}
       
    }
 
}
