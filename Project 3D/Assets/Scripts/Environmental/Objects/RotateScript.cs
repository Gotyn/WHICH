using UnityEngine;
using System.Collections;

public class RotateScript : MonoBehaviour {

	public float rotateSpeed = 1;

    void Update () {
		this.transform.Rotate (0, rotateSpeed * Time.deltaTime, 0);
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponentInChildren<DeathScript>() as DeathScript != null)
        {
            other.gameObject.GetComponentInChildren<DeathScript>().Respawn();
        }
    }
}
