using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to the GameManager (Empty GameObject).
/// </summary>

public class GameManagerScript : MonoBehaviour {

    [HideInInspector]
    public static GameObject BB;
    [HideInInspector]
    public static GameObject SB;


    void Awake() {
        DontDestroyOnLoad(InvincibleScript.Instance);
        //DontDestroyOnLoad(MenuScript.Instance);
        BB = GameObject.FindGameObjectWithTag("Big");
        SB = GameObject.FindGameObjectWithTag("Small");

       
    }
}
