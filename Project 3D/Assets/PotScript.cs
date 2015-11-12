using UnityEngine;
using System.Collections;

public class PotScript : MonoBehaviour {
	[SerializeField]
	GameObject otherPot;
	bool isAlreadyLit = false;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (otherPot.GetComponent<TorchScript> ().isLit && !isAlreadyLit) {
			this.GetComponent<TorchScript> ().ExtinguishFire ();
			isAlreadyLit = true;
		} 

		if (this.GetComponent<TorchScript> ().isLit && isAlreadyLit) {
			otherPot.GetComponent<TorchScript>().ExtinguishFire();
			isAlreadyLit = false;
		} 
	}
}
