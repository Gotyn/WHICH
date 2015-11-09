using UnityEngine;
using System.Collections;

//TriggerScript for darkroom, when you fall down
public class DeadTriggerSccript : MonoBehaviour {

	GameObject big;
	GameObject small;

	public GameObject Torch;

	void Start () {
        big = GameManagerScript.BB;
        small = GameManagerScript.SB;
    }

	void OnTriggerEnter (Collider hit) {
		if (hit.CompareTag ("Small") || hit.CompareTag ("Big") || hit.CompareTag ("Torch")) {
			small.GetComponent<CameraControlScript>().Respawn();
			big.GetComponent<CameraControlScript>().Respawn();
			big.GetComponentInChildren<HolderTest>().holdingObject = false;
			big.GetComponentInChildren<HolderTest>().holdingPlayer = false;
			Torch.GetComponent<TorchScript>().Respawn();
		}
	}
}
