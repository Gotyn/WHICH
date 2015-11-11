using UnityEngine;
using System.Collections;

public class LightPost : MonoBehaviour {

    // Components
    [SerializeField]
    DoorScript doorScript;
    Light myLight;
    ParticleSystem particles;

    
    void Start()
    {
        myLight = GetComponentInChildren<Light>();
        particles = GetComponentInChildren<ParticleSystem>();
        particles.enableEmission = true;
        myLight.intensity = 2;
    }

    // Update is called once per frame
    void Update () {

        if (doorScript != null)
        {
            if (doorScript.state == 2)
            {
                myLight.intensity = 2;
                particles.enableEmission = true;
            }
            else if (doorScript.state == 1)
            {
                myLight.intensity = 0;
                particles.enableEmission = false;
            }
        }

	}
}
