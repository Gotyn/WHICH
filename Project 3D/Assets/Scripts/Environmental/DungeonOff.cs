using UnityEngine;
using System.Collections;

public class DungeonOff : MonoBehaviour {

	bool SmallBroInPos = false;
	bool BigBroInPos = false;

    public GameObject blockWall;
	GameObject myLight;
	
	// Use this for initialization
	void Start () {
		myLight = GameObject.Find ("Directional Light");
        blockWall.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (SmallBroInPos && BigBroInPos) {
			Invoke("TurnOn",1);
            Invoke("WallOn", 2);
		}
    }
    void WallOn()
    {
        blockWall.SetActive(true);
    }

	void TurnOn () {
		RenderSettings.ambientLight = Color.white;
		myLight.SetActive(true);
       
    }
	void OnTriggerEnter(Collider hit)
	{
		if (hit.transform.CompareTag ("Big"))
			BigBroInPos = true;
		
		if (hit.transform.CompareTag ("Small"))
			SmallBroInPos = true;
		
	}
}
