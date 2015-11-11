using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// This will be used to store some settings in case we reload the scene.
/// </summary>
public class InvincibleScript : MonoBehaviour {
    private static InvincibleScript instance;
    public static bool firstLaunch = true;

    public float volume = 1.0f;
    public bool showSplash = true;
    public bool dialogsEnabled = true;
    public SetPositionOfPlayers.Positions currentPosition;


    public static InvincibleScript Instance {
        get {
            if (instance == null) {
                instance = new GameObject("InvincibleScript").AddComponent<InvincibleScript>();
            } 
            return instance;
        }
    }

    void OnLevelWasLoaded() {
        if(!firstLaunch) {
			GameObject.Find("ScreenFader").GetComponent<Image>().enabled = true;
			GameObject.Find("ScreenFader").GetComponent<Image>().color = new Color(0,0,0,1);
            Invoke("ClickPlay", .0000002f);
        }
    }

    void ClickPlay() {
        MenuScript.Instance.PlayClick();
    }

}
