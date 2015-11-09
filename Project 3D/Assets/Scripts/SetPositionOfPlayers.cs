using UnityEngine;
using System.Collections;

public class SetPositionOfPlayers : MonoBehaviour {
    public enum Positions
    {
        FirstSceneStart, FirstSceneAfterDark, SecondScene
    }
    [HideInInspector]
    public static Positions currentPos;

    public GameObject S1;
    public GameObject B1;

    public GameObject S2;
    public GameObject B2;

    

    // Use this for initialization
    void Start() {
        currentPos = InvincibleScript.Instance.currentPosition;

        switch (currentPos)
        {
            case Positions.FirstSceneStart:
                GameManagerScript.BB.transform.position = B1.transform.position;
                GameManagerScript.SB.transform.position = S1.transform.position;
                break;
            case Positions.FirstSceneAfterDark:
                GameManagerScript.BB.transform.position = B2.transform.position;
                GameManagerScript.SB.transform.position = S2.transform.position;
                break;
            case Positions.SecondScene:
              //  MenuScript.Instance.DisableAll();
                GameManagerScript.BB.transform.position = B1.transform.position;
                GameManagerScript.SB.transform.position = S1.transform.position;
                break;
        }
    }
	
}
