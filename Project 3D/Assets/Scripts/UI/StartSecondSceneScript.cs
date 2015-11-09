using UnityEngine;
using System.Collections;

public class StartSecondSceneScript : MonoBehaviour {
    MenuScript menu;
    void Start () {
        menu = MenuScript.Instance;

        Invoke("ClickPlay", .0000002f);
    }

    void ClickPlay() {
        menu.PlayClick();
    }
}
