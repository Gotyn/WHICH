using UnityEngine;
using System.Collections;

public class Log : MonoBehaviour {

    [SerializeField]
    private Transform com;

	void Start()
    {
        GetComponentInChildren<Rigidbody>().centerOfMass = com.position;
        Debug.Log(transform.parent.name);
    }
}
