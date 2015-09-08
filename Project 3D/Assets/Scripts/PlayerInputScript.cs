using UnityEngine;
using System.Collections;

public class PlayerInputScript : MonoBehaviour {
    private GameManagerScript gameManager;

    [HideInInspector] public string horizontalControls;
    [HideInInspector] public string verticalControls;
    [HideInInspector] public string interactControl_1;
    [HideInInspector] public string interactControl_2;

    // Use this for initialization
    void Start () {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();

        if (gameObject.tag == "Big") {
            switch (gameManager.inputType) {
                case GameManagerScript.InputType.UltimateArcadeMachine:
                    horizontalControls = "LEFT_ANALOG_JOYSTICK_X";
                    verticalControls = "LEFT_ANALOG_JOYSTICK_Y";
                    break;
                case GameManagerScript.InputType.Xbox360Controller:
                    horizontalControls = "LEFT_ANALOG_JOYSTICK_X";
                    verticalControls = "LEFT_ANALOG_JOYSTICK_Y";
                    break;
                case GameManagerScript.InputType.Keyboard:
                    horizontalControls = "HorizontalB";
                    verticalControls = "VerticalB";
                    break;
                default:
                    horizontalControls = "HorizontalB";
                    verticalControls = "VerticalB";
                    break;
            }
        } else {
            switch (gameManager.inputType) {
                case GameManagerScript.InputType.UltimateArcadeMachine:
                    horizontalControls = "RIGHT_ANALOG_JOYSTICK_X_UAC";
                    verticalControls = "RIGHT_ANALOG_JOYSTICK_Y_UAC";
                    break;
                case GameManagerScript.InputType.Xbox360Controller:
                    horizontalControls = "RIGHT_ANALOG_JOYSTICK_X_360";
                    verticalControls = "RIGHT_ANALOG_JOYSTICK_Y_360";
                    break;
                case GameManagerScript.InputType.Keyboard:
                    horizontalControls = "HorizontalS";
                    verticalControls = "VerticalS";
                    break;
                default:
                    horizontalControls = "HorizontalS";
                    verticalControls = "VerticalS";
                    break;
            }
        }
	}
}
