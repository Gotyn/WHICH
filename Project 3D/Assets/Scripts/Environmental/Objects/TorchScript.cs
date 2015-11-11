using UnityEngine;
using System.Collections;

/// <summary>
/// Attached to Torches and Campfires.
/// </summary>

[RequireComponent(typeof(AudioSource))]

public class TorchScript : MonoBehaviour {

	GameObject bigBro;
	GameObject smallBro;

	// visuals
    [SerializeField]
    private Light pointLight;
	[SerializeField]
	private ParticleSystem particles;
	public float TorchOnIntensity = 1;

	//audio
	private AudioSource audioFire;

	// puzzlecomponents
    [HideInInspector]
    public bool isLit = false;
    // Used when you need to know in what state the torch needs to be in, in order to complete the puzzle !
	public bool needActivated = false;
    // Tells if this torch meets the requirement to solve the puzzle.
	[HideInInspector]
    public bool completed = false;
	
	[SerializeField]
	bool On = false;

	Vector3 startPos;

    void Start()
    {
        bigBro = GameManagerScript.BB;
        smallBro = GameManagerScript.SB;
        startPos = transform.position;
        audioFire = GetComponent<AudioSource>();
        particles.enableEmission = false;
        pointLight.intensity = 0;
        if (On)
        {
            SetFire();
        }
    } 

	void Update () {
		AudioManager ();
	}

    public void SetFire() {
        particles.enableEmission = true;
        pointLight.intensity = TorchOnIntensity;
        isLit = true;
        if (audioFire != null && !audioFire.isPlaying) audioFire.Play();
	}

	public void ExtinguishFire() {
        particles.enableEmission = false;
        pointLight.intensity = 0;
		isLit = false;
        if (audioFire != null && audioFire.isPlaying) audioFire.Stop();
	}

	public void Respawn () {
		this.transform.position = startPos;
		ExtinguishFire ();
	}

	void AudioManager () {
		float distance = 0;
		float distanceToSmallBro = Vector3.Distance (this.transform.position, smallBro.transform.position);
		float distanceToBigBro = Vector3.Distance (this.transform.position, bigBro.transform.position);
		if (distanceToBigBro >= distanceToSmallBro) {
			distance = distanceToSmallBro;
		} else {
			distance = distanceToBigBro;
		}
		audioFire.volume = .5f / distance;
	}

	void OnCollisionEnter(Collision other) {
		if (other.collider.CompareTag ("Torch") && this.CompareTag("Torch")) {
			if(other.gameObject.GetComponent<TorchScript>().isLit && !this.isLit) {
				this.SetFire();
			}
		}
	}
}