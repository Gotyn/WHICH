using UnityEngine;
using System.Collections;

public class InteractableObjectMovement : MonoBehaviour
{
	// transform objects
    [SerializeField]
	Transform interactableObject;
    [SerializeField]
    Transform activeState;
    [SerializeField]
    Transform inactiveState;

	//speed/direction
    [SerializeField]
    float speed;
    Vector3 direction;

	//changes destination based on state
    public int state = 1;
	Transform destination;

	//Distance between object and endPos, might need to increase when jittering
    [HideInInspector]
    public float maxDistance;

    // Update is called once per frame
    public virtual void FixedUpdate()
    {
        interactableObject.GetComponent<Rigidbody>().MovePosition(interactableObject.position + direction * speed * Time.deltaTime);
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