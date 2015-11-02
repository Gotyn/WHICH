using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InteractionHintScript : MonoBehaviour {

    [SerializeField]
    Image explanationImage;

    // Use this for initialization
	void Start () {
        explanationImage.enabled = false;
	}
    
    void OnTriggerEnter(Collider hit) {
        if (hit.gameObject.CompareTag("SmallT") || hit.gameObject.CompareTag("BigT")) {
            explanationImage.enabled = true;
        }
    }	

    void OnTriggerExit(Collider hit) {
        if (hit.gameObject.CompareTag("SmallT") || hit.gameObject.CompareTag("BigT")) {
            explanationImage.enabled = false;
        }
    }
}
