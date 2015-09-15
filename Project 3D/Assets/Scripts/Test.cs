using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {
    Renderer myRenderer;
    Material mat;
    Color baseColor;
    Color finalColor;
    // Use this for initialization
    void Start () {
        myRenderer = GetComponent<Renderer>();
        mat = myRenderer.material;
    }

    void Update()
    {
        float emission = Mathf.PingPong(Time.time, 1.0f);
        baseColor = Color.cyan; //Replace this with whatever you want for your base color at emission level '1'

        finalColor = baseColor * Mathf.LinearToGammaSpace(emission);

        mat.SetColor("_EmissionColor", finalColor);
    }
}
