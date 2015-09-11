using UnityEngine;
using System.Collections;

/// <summary>
/// Attached to Torches and Campfires.
/// </summary>

[RequireComponent(typeof(AudioSource))]

public class TorchScript : MonoBehaviour {

	[SerializeField]
	private GameObject particles;
    private AudioSource audioFire;

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
}
