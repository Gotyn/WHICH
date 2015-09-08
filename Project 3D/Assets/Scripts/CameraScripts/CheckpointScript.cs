using UnityEngine;
using System.Collections;

public class CheckpointScript : MonoBehaviour {

    GameObject small;
    Camera cam;
    GameObject big;

	public GameObject newSpawnPointSmall;
	public GameObject newSpawnPointBig;
    
	// Use this for initialization
	void Start () {
		small = GameObject.FindGameObjectWithTag ("Small");
		big = GameObject.FindGameObjectWithTag ("Big");
        cam = Camera.main;
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider hit)
    {
            small.GetComponent<CameraControlScript>().spawn = newSpawnPointSmall;
            big.GetComponent<CameraControlScript>().spawn = newSpawnPointBig;
            cam.GetComponent<CameraSpline>().MoveToNext();
            this.gameObject.SetActive(false);
    }
}
