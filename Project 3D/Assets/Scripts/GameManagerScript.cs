using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to the GameManger (Empty GameObject).
/// 
/// It takes care of the inputtype and makes it possible to switch
/// between those.
/// </summary>

public class GameManagerScript : MonoBehaviour {
    public InputType inputType;

    public enum InputType {
        UltimateArcadeMachine,
        Xbox360Controller,
        Keyboard
    }
}
