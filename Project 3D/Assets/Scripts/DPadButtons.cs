using UnityEngine;
using System.Collections;

public class DPadButtons : MonoBehaviour {
    GameManagerScript gameManager;

    private string horizontalDPad;
    private string verticalDPad;

    public static bool up;
    public static bool down;
    public static bool left;
    public static bool right;

    float lastX;
    float lastY;
     
    void Start() {
        gameManager = GetComponent<GameManagerScript>();

        switch (gameManager.inputType) {
            case GameManagerScript.InputType.UltimateArcadeMachine:
                horizontalDPad = "DPAD_X_UAC";
                verticalDPad = "DPAD_Y_UAC";
                break;
            case GameManagerScript.InputType.Xbox360Controller:
                horizontalDPad = "DPAD_X_360";
                verticalDPad = "DPAD_Y_360";
                break;
            default:
                horizontalDPad = "DPAD_X_360";
                verticalDPad = "DPAD_Y_360";
                break;
        }
    }

    // Update is called once per frame
    void Update () {
        float lastDpadX = lastX;
        float lastDpadY = lastY;

        if (Input.GetAxis(horizontalDPad) == 1 && Input.GetAxis(verticalDPad) == 1 ||
            Input.GetAxis(horizontalDPad) == 1 && Input.GetAxis(verticalDPad) == -1 ||
            Input.GetAxis(horizontalDPad) == -1 && Input.GetAxis(verticalDPad) == -1 ||
            Input.GetAxis(horizontalDPad) == -1 && Input.GetAxis(verticalDPad) == 1) { return; }

        if (Input.GetAxis(horizontalDPad) == 1 && lastDpadX != 1) { right = true; lastX = 1; } else { right = false; }
        if (Input.GetAxis(horizontalDPad) == -1 && lastDpadX != -1) { left = true; lastX = -1; } else { left = false; }

        if (Input.GetAxis(verticalDPad) == 1 && lastDpadY != 1) { up = true; lastY = 1; } else { up = false; }
        if (Input.GetAxis(verticalDPad) == -1 && lastDpadY != -1) { down = true; lastY = -1; } else { down = false; }

        if (Input.GetAxis(horizontalDPad) == 0) { lastX = 0; }
        if (Input.GetAxis(verticalDPad) == 0) { lastY = 0; }
    }
}
