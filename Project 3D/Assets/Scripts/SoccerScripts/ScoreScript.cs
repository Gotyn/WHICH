using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

	public int bScore = 0;
	public int sScore = 0;

	public Text bText;
	public Text sText;

	GameObject ball;
	Vector3 startPos;
	
	// Use this for initialization
	void Start () {
		ball = GameObject.FindGameObjectWithTag("Ball");
		startPos = ball.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		bText.text = bScore.ToString ();
		sText.text = sScore.ToString ();
	}

	//respawn the ball
	public void Respawn () {
		ball.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		ball.transform.position = startPos;
	}
}
