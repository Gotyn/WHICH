using UnityEngine;
using System.Collections;

public class BigBroGlow : MonoBehaviour
{

    public bool sInPos = false;
    public bool bInPos = false;
    ParticleSystem pS;
    void Start()
    {
        pS = transform.root.GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        if (sInPos && bInPos)
        {
            pS.enableEmission = true;
        }
        else
        {
            pS.enableEmission = false;
        }
    }


}
