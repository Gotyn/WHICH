using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneFadeInOut : MonoBehaviour {

	public float fadeSpeed = 1.5f;

	//[HideInInspector]
	public bool fade = false;

	Image texture;

	void Awake () {
		texture = GetComponent<Image> ();

	}

	void Update () {
		if (fade) {
			EndScene ();
		}
	}

	void FadeToClear () {
		texture.color = Color.Lerp (texture.color, Color.clear, fadeSpeed * Time.deltaTime);
	}

	void FadeToBlack () {
		texture.color = Color.Lerp (texture.color, Color.black, fadeSpeed * Time.deltaTime);
	}

	public void StartScene () {
		FadeToClear ();
		Debug.Log (texture.color.a);

		if (texture.color.a <= 0.05f) {
			texture.color = Color.clear;
			texture.enabled = false;
			//fade = false;
		}
	}

	public void EndScene () {
		texture.enabled = true;
		FadeToBlack ();

		if (texture.color.a >= 0.995f) {
			texture.color = Color.black;
			Invoke("LoadLevel",3);
		}
	}

	void LoadLevel () {
		Application.LoadLevel(0);
	}
}
