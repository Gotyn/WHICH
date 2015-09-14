using UnityEngine;
using System.Collections;

/// <summary>
/// Attached to Torches and Campfires.
/// </summary>

[RequireComponent(typeof(AudioSource))]

public class TorchScript : MonoBehaviour {
    private GameManagerScript gameManager; //to check the average position of the players.

	[SerializeField]
	private GameObject particles;
    private AudioSource audioFire;

    private float distanceToPlayers;

    [HideInInspector]
    public bool isLit = false;

    // Used when you need to know in what state the torch needs to be in, in order to complete the puzzle !
    public bool needActivated = false;
    [HideInInspector]
    
    // Tells if this torch meets the requirement to solve the puzzle.
    public bool completed = false;

    void Start()
    {
        audioFire = GetComponent<AudioSource>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();

    }

    // Update is called once per frame
    public void SetFire() {
		particles.SetActive (true);
        isLit = true;
        if (audioFire != null) audioFire.Play();
	}

	public void ExtinguishFire() {
		particles.SetActive (false);
		isLit = false;
        if (audioFire != null)audioFire.Stop();
	}

    void Update() {
        distanceToPlayers = Vector3.Distance(transform.position, gameManager.PlayersAveragePosition);
        Debug.Log("dis2p " + distanceToPlayers + "   " + name);
        if (distanceToPlayers > 50 && isLit && audioFire.isPlaying) {
            audioFire.volume -= 0.1f * Time.deltaTime;
        }

        if (distanceToPlayers < 50 && isLit && audioFire.isPlaying) {
            if (audioFire.volume >= 0.2f) {
                audioFire.volume = 0.2f;
            } else {
                audioFire.volume += 0.1f * Time.deltaTime;
            }
        }
    }
}
