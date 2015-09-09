using UnityEngine;
using System.Collections;

public class TorchScript : MonoBehaviour {

	[SerializeField]
	GameObject particles;

    [HideInInspector]
    public bool isLit = false;

    AudioSource aTorch;
    /// <summary>
    /// Used when you need to know in what state the torch needs to be in, in order to complete the puzzle !
    /// </summary>
    public bool needActivated = false;
    [HideInInspector]
    /// <summary>
    /// Tells if this torch meets the requirement to solve the puzzle.
    /// </summary>
    public bool completed = false;

    void Start()
    {
        aTorch = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void SetFire() {
		particles.SetActive (true);
        isLit = true;
        //aTorch.Play();
	}

	public void ExtinguishFire() {
		particles.SetActive (false);
		isLit = false;
        //aTorch.Stop();
	}
}
