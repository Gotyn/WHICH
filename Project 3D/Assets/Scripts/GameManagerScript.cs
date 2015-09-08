using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {
    public InputType inputType;

    public enum InputType {
        UltimateArcadeMachine,
        Xbox360Controller,
        Keyboard
    }
}
