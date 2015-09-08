using UnityEngine;
using System.Collections;

public class WanderingAI : MonoBehaviour {

    float moveSpeed = 2;
    Vector3 wayPoint;
    float wanderRange = 5;
    [SerializeField]
    Transform pointOfWander;


    void Start()
    {
        StartWandering();
    }
	
    void StartWandering()
    {
        
        wayPoint = wayPoint = new Vector3(Random.Range(pointOfWander.position.x, pointOfWander.position.x + wanderRange), 1, Random.Range(pointOfWander.position.z, pointOfWander.position.z + wanderRange));
        wayPoint.y = 1;
        transform.LookAt(wayPoint);
    }
	// Update is called once per frame
	void Update () {
        transform.position += transform.TransformDirection(Vector3.forward * moveSpeed * Time.deltaTime);

        if ((transform.position - wayPoint).magnitude < 1)
        {
            Debug.Log("lelel");
            StartWandering();
        }
    }
}
