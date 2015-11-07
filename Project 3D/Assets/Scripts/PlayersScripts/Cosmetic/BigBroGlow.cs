using UnityEngine;
using System.Collections;

public class BigBroGlow : MonoBehaviour
{
    public bool smallBroInPos = false;
    public bool bigBroInPos = false;
    ParticleSystem partSystem;

    void Start()
    {
        partSystem = transform.root.GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        if (smallBroInPos && bigBroInPos)
        {
            partSystem.enableEmission = true;
        }
        else
        {
            partSystem.enableEmission = false;
        }
    }
}
