using UnityEngine;
using System.Collections;

public class PressurePlateFix : MonoBehaviour {

	[SerializeField]
    Transform pressurePlateTrigger;

    public bool triggered = false;
    

    void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.CompareTag("Box") || hit.gameObject.CompareTag("Big"))
        {
            triggered = true;
        }
    }

    void OnTriggerExit(Collider hit)
    {
        if (hit.gameObject.CompareTag("Box") || hit.gameObject.CompareTag("Big"))
        {
            triggered = false;
        }
    }
}
