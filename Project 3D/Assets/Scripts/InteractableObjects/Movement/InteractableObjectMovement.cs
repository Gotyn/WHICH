using UnityEngine;
using System.Collections;

public class InteractableObjectMovement : MonoBehaviour
{
    [SerializeField]
    Transform interactableObject;

    [SerializeField]
    Transform activeState;

    [SerializeField]
    Transform inactiveState;

    [SerializeField]
    float speed;

    Vector3 direction;
    Transform destination;

    public int state = 1;

    [HideInInspector]
    public float maxDistance;

    // Update is called once per frame
    public virtual void FixedUpdate()
    {
        interactableObject.GetComponent<Rigidbody>().MovePosition(interactableObject.position + direction * speed * Time.deltaTime);
        //  Debug.Log(" value + " + onPlatform);
        if (state == 2)
        {

            SetDestination(activeState);

        }

        if (state == 1)
        {

            SetDestination(inactiveState);

        }

        if (Vector3.Distance(interactableObject.position, destination.position) < maxDistance)
        {
         //   Debug.Log("Distance reached");
            state = 0;
            direction = Vector3.zero;
        }
    }


	// set destination of object to move
    void SetDestination(Transform dest)
    {
        destination = dest;
        direction = (destination.position - interactableObject.position).normalized;

    }


}