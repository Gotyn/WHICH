using UnityEngine;
using System.Collections;

public class SBGlowing : MonoBehaviour {
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
        baseColor = Color.cyan;

        finalColor = baseColor * Mathf.LinearToGammaSpace(emission);

        mat.SetColor("_EmissionColor", finalColor);
    }
}
