using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to the GameManager (Empty GameObject).
/// </summary>

public class GameManagerScript : MonoBehaviour {
    void Start() {
        DontDestroyOnLoad(InvincibleScript.Instance);
    }
}
