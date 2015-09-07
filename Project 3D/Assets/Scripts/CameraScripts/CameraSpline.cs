using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraSpline : MonoBehaviour {

	public GameObject path;
	Transform [] m_transfroms;
	bool reverse = false;

	public float speed = 1.0f;
	float startTime;
	float length;

	int index = 0;
	int previousIndex = 0;
	// Use this for initialization
	void Start () {
		m_transfroms = GetTransforms ();

		Camera.main.transform.position = m_transfroms [index].position;
		Camera.main.transform.rotation = m_transfroms [index].rotation;
	}
	// get children of path gameobject and put them in a list
	Transform[] GetTransforms () {
		if (path != null) {
			List<Component> components = new List<Component>(path.GetComponentsInChildren(typeof(Transform)));
			List<Transform> transforms = components.ConvertAll(c => (Transform)c);

			transforms.Remove(path.transform);

			return transforms.ToArray();
		}
		return null;
	}

	// move to next camera position
	public void MoveToNext() {
		previousIndex = index;
		index++;
		startTime = Time.time;
		length = Vector3.Distance (m_transfroms [index - 1].position, m_transfroms [index].position);
	}

	//move to specific camera position
	public void MoveTo(int pIndex) {
		previousIndex = index;
		index = pIndex;
		startTime = Time.time;
		length = Vector3.Distance (m_transfroms [index].position, m_transfroms [previousIndex].position);
	}


	// makes the camera lerp between to camera positions based on index
	void CameraMovement() {
		float distCovered = (Time.time - startTime) * speed;
		float completed = distCovered / length;
		
		if (index - 1 >= 0) {
			Camera.main.transform.position = Vector3.Lerp (m_transfroms [previousIndex].position, m_transfroms [index].position, completed);
			Camera.main.transform.rotation = Quaternion.Lerp (m_transfroms [previousIndex].rotation, m_transfroms [index].rotation, completed);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			MoveToNext ();
		}

		CameraMovement ();
	}
}
