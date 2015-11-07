using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to the GameManager (so it's always there).
/// It checks whether the DPAD buttons have been pressed and converts 
/// axis to button presses.
/// 
/// The script is accessed by multiple scripts, basicly on all scripts
/// that handle interaction with the BigBro.
/// </summary>

public class DPadButtons : MonoBehaviour {
    private string horizontalDPad;
    private string verticalDPad;

    public static bool up;
    public static bool down;
    public static bool left;
    public static bool right;

    float lastX;
    float lastY;

    void Start() {
        horizontalDPad = "BIG_INTERACT_2";
        verticalDPad = "BIG_INTERACT_1";
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
