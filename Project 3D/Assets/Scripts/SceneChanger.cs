using UnityEngine;
using System.Collections;

public class SceneChanger : MonoBehaviour {

    bool smallIn = false;
    bool bigIn = false;
    bool sceneSwitchActivated = false;
    bool invokeCalled = false;

    public int whichScene = 0;

    void Update()
    {
        if(smallIn && bigIn && !sceneSwitchActivated)
        {
            InvincibleScript.Instance.showSplash = false;
			GameObject.Find("ScreenFader").GetComponent<SceneFadeInOut>().fade = true;

            if (!invokeCalled) {
                Invoke("ChangeScene", 4f);
                invokeCalled = true;
            }
          
        }
    }

	void ChangeScene () {
        sceneSwitchActivated = true;
        if (whichScene == 1) InvincibleScript.Instance.currentPosition = SetPositionOfPlayers.Positions.SecondScene;
        if (whichScene == 0) InvincibleScript.Instance.currentPosition = SetPositionOfPlayers.Positions.FirstSceneAfterDark;

        Application.LoadLevel(whichScene);
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
