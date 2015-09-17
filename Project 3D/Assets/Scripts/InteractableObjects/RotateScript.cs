using UnityEngine;
using System.Collections;

public class RotateScript : MonoBehaviour {

	public float rotateSpeed = 1;

	void Update () {
	
		this.transform.Rotate (0, rotateSpeed * Time.deltaTime, 0);
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<CameraControlScript>() as CameraControlScript != null)
        {
            other.gameObject.GetComponent<CameraControlScript>().Respawn();
            Debug.Log("Gay");
        }
    }
}
