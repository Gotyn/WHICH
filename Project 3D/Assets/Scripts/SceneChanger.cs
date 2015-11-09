using UnityEngine;
using System.Collections;

public class SceneChanger : MonoBehaviour {

    //GameObject BB;
    //GameObject SB;
    bool smallIn = false;
    bool bigIn = false;
    bool changed = false;
    // Use this for initialization
	void Start () {
        //BB = GameManagerScript.BB;
        //SB = GameManagerScript.SB;
        
	}
	

    void Update()
    {
        if(smallIn && bigIn && !changed)
        {
            InvincibleScript.Instance.currentPosition = SetPositionOfPlayers.Positions.SecondScene;
            InvincibleScript.Instance.showSplash = false;
            
            changed = true;
            Application.LoadLevel(1);
        }
    }
	void OnTriggerEnter(Collider hit)
    {
        if (hit.CompareTag("Small"))
        {
            smallIn = true;
        }
        if (hit.CompareTag("Big"))
        {
            bigIn = true;
        }
    }
    void OnTriggerExit(Collider hit)
    {
        if (hit.CompareTag("Small"))
        {
            smallIn = false;
        }
        if (hit.CompareTag("Big"))
        {
            bigIn = false;
        }
    }
}
