using UnityEngine;
using System.Collections.Generic;

public class WanderingAI : MonoBehaviour {


    //Components
    NavMeshAgent navigation;
    [SerializeField]
    GameObject path;
    Transform[] wayPoints;
  
    //Variables
    int currentWayPoint = 0;
    int maxWaypoint;
    float checkPointReach = 0.5f;


    void Start()
    {
        wayPoints = GetTransforms();
        navigation = GetComponent<NavMeshAgent>();
        maxWaypoint = wayPoints.Length - 1;
    }

    Transform[] GetTransforms()
    {
        if (path != null)
        {
            List<Component> components = new List<Component>(path.GetComponentsInChildren(typeof(Transform)));
            List<Transform> transforms = components.ConvertAll(c => (Transform)c);

            transforms.Remove(path.transform);

            return transforms.ToArray();
        }
        return null;
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
                //Debug.Log("reached finish");
            }
            else
            {
                currentWayPoint++;
               // Debug.Log("Next.");
            }
        }
        navigation.SetDestination(nextWayPoint);
    }

}
