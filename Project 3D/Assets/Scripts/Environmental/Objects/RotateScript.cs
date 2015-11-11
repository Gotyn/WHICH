using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
public class RotateScript : MonoBehaviour {

	GameObject bigBro;
	GameObject smallBro;

	public float rotateSpeed = 1;	
	//audio
	private AudioSource audioFire;
	
	void Start () {
		bigBro = GameObject.FindGameObjectWithTag("Big");
		smallBro = GameObject.FindGameObjectWithTag("Small");

		audioFire = GetComponent<AudioSource>();
	}

    void Update () {
		this.transform.Rotate (0, rotateSpeed * Time.deltaTime, 0);
		AudioManager ();
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponentInChildren<DeathScript>() as DeathScript != null)
        {
            other.gameObject.GetComponentInChildren<DeathScript>().Respawn();
        }
    }

	void AudioManager () {
		float distance = 0;
		float distanceToSmallBro = Vector3.Distance (this.transform.position, smallBro.transform.position);
		float distanceToBigBro = Vector3.Distance (this.transform.position, bigBro.transform.position);
		if (distanceToBigBro >= distanceToSmallBro) {
			distance = distanceToSmallBro;
		} else {
			distance = distanceToBigBro;
		}
		audioFire.volume = 5/distance;
	}
}
