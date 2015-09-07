using UnityEngine;
using System.Collections;

public class FirePlaceScript : MonoBehaviour {

	[SerializeField]
	GameObject particles;

	[HideInInspector]
	public bool isLit = false;

	public void SetFire() {
		particles.SetActive (true);
		isLit = true;
	}
	
	public void ExtinguishFire() {
		particles.SetActive (false);
		isLit = false;
	}
}
