using UnityEngine;
using System.Collections;

public class DeathScript : MonoBehaviour {

    PlayerMovement playerMovement;
    Transform mainTranform;
    CameraControlScript camControl;
    [SerializeField]
    GameObject smokePrefab;
    [HideInInspector]
    public bool respawned = false;

    
	// Use this for initialization
	void Start () {
        camControl = GetComponentInParent<CameraControlScript>();
        playerMovement = GetComponentInParent<PlayerMovement>();
        mainTranform = transform.parent;
	}

    void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.CompareTag("Enemy"))
        {
            GameObject go = Instantiate(smokePrefab, transform.position, Quaternion.identity) as GameObject;
            Destroy(go, 1);
            camControl.enabled = false;
            mainTranform.position = new Vector3(100, 0, 100);
            respawned = false;
            playerMovement.enabled = false;
            StartCoroutine(respawn());
        }
    }

    IEnumerator respawn()
    {
        yield return new WaitForSeconds(1f);
        camControl.enabled = true;
        yield return new WaitForSeconds(1.5f);
        respawned = true;
        if (playerMovement.gameObject.CompareTag("Big")) playerMovement.enabled = true; //fix for big guy.
    }
}
