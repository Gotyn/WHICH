using UnityEngine;
using System.Collections;

public class BallPuzzle : MonoBehaviour {
    public GameObject pot;

	void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            pot.GetComponent<TorchScript>().SetFire();
        }
    }
}
