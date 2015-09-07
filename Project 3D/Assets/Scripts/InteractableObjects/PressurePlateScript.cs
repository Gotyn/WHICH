using UnityEngine;
using System.Collections;

public class PressurePlateScript : MonoBehaviour {

    /// <summary>
    /// Used when you need to know in what state the pressure plate needs to be in order to complete the puzzle !
    /// </summary>
    public bool needActivated = false;
    [HideInInspector]
    /// <summary>
    /// Tells if the current pressure plate is active or not.
    /// </summary>
    public bool activated = false;
    [HideInInspector]
    /// <summary>
    /// Tells if this pressure plate meets the requirement to solve the puzzle.
    /// </summary>
    public bool completed = false;
    /// <summary>
    /// Second trigger collider, in order to avoid activate / deactivate glitches.
    /// </summary>
    PressurePlateFix fix;
    /// <summary>
    /// Animation of the moving platform.
    /// </summary>
    InteractableObjectMovement movement; 
    
    void Start()
    {
        movement = GetComponentInParent<PressurePlateMovement>();
        fix = GetComponentInChildren<PressurePlateFix>();
        
    }
    
    void OnTriggerEnter(Collider hit) {
        if (hit.gameObject.CompareTag("Box") || hit.gameObject.CompareTag("Big"))
        {
            if (!fix.triggered)
            {
                movement.state = 2;
                activated = true;
            }
        }
    }
   
    void OnTriggerExit(Collider hit)
    {
        if (hit.gameObject.CompareTag("Box") || hit.gameObject.CompareTag("Big"))
        {
            if (!fix.triggered)
            {
                movement.state = 1;
                activated = false;
            }
        }
    }


}
