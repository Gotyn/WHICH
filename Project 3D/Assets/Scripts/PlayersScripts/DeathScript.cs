using UnityEngine;
using System.Collections;

public class DeathScript : MonoBehaviour {

    PlayerMovement playerMovement;
    CameraControlScript camControl;
    [SerializeField]
    GameObject smokePrefab;
    [HideInInspector]
    public bool respawned = true;

    HolderTest holderTest;

    void Start () {
        holderTest = FindObjectOfType<HolderTest>();
        camControl = GetComponentInParent<CameraControlScript>();
        playerMovement = GetComponentInParent<PlayerMovement>();
	}

    void OnTriggerEnter(Collider hit)
    {
        if (hit.CompareTag("Log"))
        {
            Respawn();
            Destroy(hit.gameObject);
        }
    }
    public void Respawn()
    {
        GetComponentInParent<PlayerMovement>().enabled = false;
        GameObject go = Instantiate(smokePrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy(go, 1);
        respawned = false;
        playerMovement.enabled = false;
        if (transform.gameObject.CompareTag("SmallT")) { holderTest.holdingPlayer = false; }
        camControl.Respawn();
    }
}
