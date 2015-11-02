using UnityEngine;
using System.Collections;

public class QuestionMarkRotator : MonoBehaviour {
    float rotatingSpeed = 65;
	// Update is called once per frame
	void Update () {
        transform.Rotate(0,Time.deltaTime * rotatingSpeed, 0);
	}
}
