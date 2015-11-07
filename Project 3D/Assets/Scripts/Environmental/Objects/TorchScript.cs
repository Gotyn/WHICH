using UnityEngine;
using System.Collections;

/// <summary>
/// Attached to Torches and Campfires.
/// </summary>

[RequireComponent(typeof(AudioSource))]

public class TorchScript : MonoBehaviour {


  
	// visuals
    [SerializeField]
    private Light pointLight;
	[SerializeField]
	private ParticleSystem particles;
	public float intensity = 1;

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
		startPos = this.transform.position;
        audioFire = GetComponent<AudioSource>();
        particles.enableEmission = false;
        if (On) SetFire();
    }

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

	public void Respawn () {
		this.transform.position = startPos;
		ExtinguishFire ();
	}
}