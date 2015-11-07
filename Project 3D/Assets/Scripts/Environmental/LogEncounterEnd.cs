using UnityEngine;
using System.Collections;

public class LogEncounterEnd : MonoBehaviour {
    void OnTriggerEnter(Collider hit) {
        if(hit.CompareTag("Small") || hit.CompareTag("Big"))
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
