using UnityEngine;
using System.Collections;

public class EnableCircle : MonoBehaviour {
    [SerializeField]
    GameObject campFire;
    SpriteRenderer sRenderer;

	// Use this for initialization
	void Start () {
        sRenderer = GetComponent<SpriteRenderer>();
        sRenderer.enabled = false;
	}

    void Update()
    {
        if (campFire.GetComponent<TorchScript>().isLit)
        {
          
            sRenderer.enabled = true;
        }
        else
        {
            sRenderer.enabled = false;
        }
    }
	
	
}
