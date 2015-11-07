using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {
	public int bigBroScore = 0;
	public int smallBroScore = 0;

	public TextMesh bigBroText;
	public TextMesh smallBroText;
	public TextMesh doorOpenedText;

	public GameObject pot;

	GameObject ball;
	Vector3 startPos;
	
	void Start () {
		ball = GameObject.FindGameObjectWithTag("Ball");
		startPos = ball.transform.position;
	}
	
	void Update () {
		bigBroText.text = bigBroScore.ToString ();
		smallBroText.text = smallBroScore.ToString ();

		if (bigBroScore >= 1 && smallBroScore >= 1) {
			pot.GetComponent<TorchScript>().SetFire();
			doorOpenedText.gameObject.SetActive(true);
		}
	}

	// Respawn the ball
	public void Respawn () {
		ball.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		ball.transform.position = startPos;
	}
}
