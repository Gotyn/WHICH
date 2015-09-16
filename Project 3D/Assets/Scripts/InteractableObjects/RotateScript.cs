using UnityEngine;
using System.Collections;

public class RotateScript : MonoBehaviour {

	public int rotateSpeed = 1;

	void Update () {
	
		this.transform.Rotate (0, rotateSpeed, 0);
	}

	void OnCollisionEnter (Collision other) {
		if (other.collider.CompareTag ("Big") || other.collider.CompareTag ("Small")) {
			other.gameObject.GetComponent<CameraControlScript>().Respawn();
		}
	}
}
