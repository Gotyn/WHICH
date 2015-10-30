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
    
    void OnTriggerEnter() {
        explanationImage.enabled = true;
    }	

    void OnTriggerExit() {
        explanationImage.enabled = false;
    }
}
