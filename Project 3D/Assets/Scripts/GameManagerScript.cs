using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to the GameManager (Empty GameObject).
/// </summary>

public class GameManagerScript : MonoBehaviour {
    private Vector3 playersAveragePosition;
    private Transform smallPlayer, bigPlayer;

    public int currentPuzzle = 1;

    public Vector3 PlayersAveragePosition {
        get { return playersAveragePosition; }
    }

    void Start() {
        smallPlayer = GameObject.FindGameObjectWithTag("Small").transform;
        bigPlayer = GameObject.FindGameObjectWithTag("Big").transform;

        DontDestroyOnLoad(InvincibleScript.Instance);
    }

    void Update() {
        playersAveragePosition = (smallPlayer.position + bigPlayer.position) / 2;
    }
}
