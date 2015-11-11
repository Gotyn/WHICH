using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// This script is attached to the GameManager (Empty GameObject).
/// </summary>

public class GameManagerScript : MonoBehaviour {

    public static GameObject BB;
    public static GameObject SB;
    public static bool gameCompleted = false;
    public static MenuScript menuScript;

    Image completedGameImage;


    void Awake() {
        DontDestroyOnLoad(InvincibleScript.Instance);
        //DontDestroyOnLoad(MenuScript.Instance);
        BB = GameObject.FindGameObjectWithTag("Big");
        SB = GameObject.FindGameObjectWithTag("Small");
    }

    void Start() {
        completedGameImage = GameObject.Find("GameCompletedImage").GetComponent<Image>();
        completedGameImage.enabled = false;

        //menuScript = GameObject.FindGameObjectWithTag("Menu").GetComponent<MenuScript>();  //Can't be in awake, because menu is created after awake.
    }

    void Update() {
        if (gameCompleted) {
            completedGameImage.enabled = true;
            
            if (Input.anyKeyDown) {
                completedGameImage.enabled = false;
                gameCompleted = false;
                MenuScript.Instance.QuitYes();
            }
        }
    }
}
