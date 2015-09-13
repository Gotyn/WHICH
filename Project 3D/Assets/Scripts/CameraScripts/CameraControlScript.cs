using UnityEngine;
using System.Collections;

public class CameraControlScript : MonoBehaviour {
	
	Camera cam;
	public GameObject spawn;
    [SerializeField]
    GameObject spawnParticle;

    //maybe move to a spawnscript.
    [HideInInspector]
    public bool spawned = false;
	// Use this for initialization
	void Start () {
		cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        CamControl();
	}

	//check if player is outside screen pos
    void CamControl()
    {
        Vector3 screenCoord = cam.WorldToScreenPoint(this.transform.position);

        if (screenCoord.x < -20 || screenCoord.x > Screen.width + 20 || screenCoord.y < -20 || screenCoord.y > Screen.height + 20)
        {
            transform.position = spawn.transform.position;
            GameObject go = Instantiate(spawnParticle, spawn.transform.position, spawn.transform.rotation) as GameObject;
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponentInChildren<DeathScript>().respawned = false;
            StartCoroutine(enabled());
            Destroy(go, 1.5f);
        }
    }
    IEnumerator enabled()
    {
        yield return new WaitForSeconds(1.5f);
        GetComponent<PlayerMovement>().enabled = true;
        GetComponentInChildren<DeathScript>().respawned = true;
    }


}
