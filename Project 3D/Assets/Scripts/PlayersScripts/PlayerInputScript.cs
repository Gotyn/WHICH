using UnityEngine;
using System.Collections;

/// <summary>
/// Attached to both Players.
/// 
/// Checks whichs input needs to be used depending on the 
/// inputdevice.
/// </summary>

public class PlayerInputScript : MonoBehaviour {

    [HideInInspector] public string horizontalControls;
    [HideInInspector] public string verticalControls;
    [HideInInspector] public string interactControl_1;
    [HideInInspector] public string interactControl_2;

    // Use this for initialization
    void Start () {

        if (gameObject.tag == "Big") {
            //Bigbro
            interactControl_1 = "BIG_INTERACT_1";
            interactControl_2 = "BIG_INTERACT_2";

            horizontalControls = "BIG_HORIZONTAL";
            verticalControls = "BIG_VERTICAL";

        } else {
            //SmallBro
            interactControl_1 = "SMALL_INTERACT_1";
            interactControl_2 = "SMALL_INTERACT_2";

            horizontalControls = "SMALL_HORIZONTAL";
            verticalControls = "SMALL_VERTICAL";
        }
	}
}
