using UnityEngine;
using System.Collections;

public class DeathScript : MonoBehaviour {

    Transform mainTranform;
    CameraControlScript camControl;
    [SerializeField]
    GameObject smokePrefab;

	// Use this for initialization
	void Start () {
        camControl = GetComponentInParent<CameraControlScript>();
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
            StartCoroutine(respawn());
        }
    }

    IEnumerator respawn()
    {
        yield return new WaitForSeconds(1f);
        camControl.enabled = true;
    }
}
