using UnityEngine;
using System.Collections;

public class WanderingAI : MonoBehaviour {

    NavMeshAgent navigation;

    float moveSpeed = 2;
    Vector3 wayPoint;
    float wanderRange = 5;
    [SerializeField]
    Transform pointOfWander;
    [SerializeField]
    Transform[] wayPoints;
    [SerializeField]
    int currentWayPoint = 0;
    [SerializeField]
    int maxWaypoint;

    float checkPointReach = 0.5f;


    void Start()
    {
        StartWandering();
        navigation = GetComponent<NavMeshAgent>();
        maxWaypoint = wayPoints.Length - 1;
    }
	
    void StartWandering()
    {
        
        wayPoint = wayPoint = new Vector3(Random.Range(pointOfWander.position.x, pointOfWander.position.x + wanderRange), 1, Random.Range(pointOfWander.position.z, pointOfWander.position.z + wanderRange));
        wayPoint.y = 1;
        transform.LookAt(wayPoint);
    }
	// Update is called once per frame
	void Update () {
       
        Patrol();
        //  transform.position += transform.TransformDirection(Vector3.forward * moveSpeed * Time.deltaTime);
       
        //if ((transform.position - wayPoint).magnitude < 1)
        //{
        //    Debug.Log("lelel");
        //    StartWandering();
        //}
        
    }

    void Patrol()
    {
        Vector3 position = transform.position;
        Vector3 nextWayPoint = wayPoints[currentWayPoint].position;
        nextWayPoint.y = transform.position.y;

        Vector3 direction = nextWayPoint - transform.position;
        
        if (Vector3.Distance(position,nextWayPoint) <= checkPointReach)
        {
            if(currentWayPoint == maxWaypoint)
            {
                currentWayPoint = 0; // if we reached last destination , go to first one.
                Debug.Log("reached finish");
            }
            else
            {
                currentWayPoint++;
                Debug.Log("Next.");
            }
        }
        navigation.SetDestination(nextWayPoint);
    }

    //public void AIPatrol()
    //{
    //    Vector3 _position = transform.position;
    //    isPatrolling = true;
    //    //renderer.material.color = Color.green; // PATROLLING
    //    navigation.stoppingDistance = 1;
    //    navigation.speed = patrolSpeed;
    //    Vector3 _nextWayPoint;
    //    _nextWayPoint = Waypoints[_currentWayPoint].position;

    //    if (Vector3.Distance(_position, _nextWayPoint) <= CheckpointReach)
    //    {
    //        if (_currentWayPoint == maxWaypoint)
    //        {
    //            // Debug.Log("----------------------------------LAST POINT REACHED ----- > Repeating!");
    //            _currentWayPoint = 0;
    //        }
    //        else
    //        {
    //            //Debug.Log("----------------------------------WAYPOINT REACHED ------- > " + _currentWayPoint);
    //            _currentWayPoint++;
    //            //Debug.Log("                                  GOING TO --------------- > " + _currentWayPoint);
    //        }
    //    }
    //    navigation.SetDestination(Waypoints[_currentWayPoint].position);
    //}
}
