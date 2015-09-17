using UnityEngine;
using System.Collections;

/// <summary>
/// Attached to Torches and Campfires.
/// </summary>

[RequireComponent(typeof(AudioSource))]

public class TorchScript : MonoBehaviour {
    private GameManagerScript gameManager; //to check the average position of the players.

	[SerializeField]
	private ParticleSystem particles;
    private AudioSource audioFire;
    [SerializeField]
    private Light pointLight;

    private float distanceToPlayers;

    [HideInInspector]
    public bool isLit = false;
    public float fadeDistance = 40.0f;
    [SerializeField]
    bool On = false;
    // Used when you need to know in what state the torch needs to be in, in order to complete the puzzle !
    public bool needActivated = false;
    [HideInInspector]
    
    // Tells if this torch meets the requirement to solve the puzzle.
    public bool completed = false;

	public float intensity = 1;

	Vector3 startPos;

    void Start()
    {
		startPos = this.transform.position;
        audioFire = GetComponent<AudioSource>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
        particles.enableEmission = false;
        if (On) SetFire();
    }

    // Update is called once per frame
    public void SetFire() {
        particles.enableEmission = true;
        pointLight.intensity = intensity;
        isLit = true;
        if (audioFire != null && !audioFire.isPlaying) audioFire.Play();
	}

	public void ExtinguishFire() {
        particles.enableEmission = false;
        pointLight.intensity = 0;
		isLit = false;
        if (audioFire != null && audioFire.isPlaying) audioFire.Stop();
	}

    void Update() {
        distanceToPlayers = Vector3.Distance(transform.position, gameManager.PlayersAveragePosition);
        if (distanceToPlayers > fadeDistance && isLit && audioFire.isPlaying) {
            audioFire.volume -= 0.1f * Time.deltaTime;
        }

        if (distanceToPlayers < fadeDistance && isLit && audioFire.isPlaying) {
            if (audioFire.volume >= 0.2f) {
                audioFire.volume = 0.2f;
            } else {
                audioFire.volume += 0.1f * Time.deltaTime;
            }
        }
    }

	public void Respawn () {
		this.transform.position = startPos;
		ExtinguishFire ();
	}
}
