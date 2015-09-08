using UnityEngine;
using System.Collections;

public class BigBroMovement : MonoBehaviour
{
    private GameManagerScript gameManager;
    private Camera cam;
    private Rigidbody rigibody;
    public float speed = 10.0f;
    private float gravity = 10.0f;
    private float maxVelocityChange = 10.0f;

    private string horizontalControls;
    private string verticalControls;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
        cam = Camera.main;
        rigibody = GetComponent<Rigidbody>();

        switch (gameManager.inputType) {
            case GameManagerScript.InputType.UltimateArcadeMachine:
                horizontalControls = "LEFT_ANALOG_JOYSTICK_X";
                verticalControls = "LEFT_ANALOG_JOYSTICK_Y";
                break;
            case GameManagerScript.InputType.Xbox360Controller:
                horizontalControls = "LEFT_ANALOG_JOYSTICK_X";
                verticalControls = "LEFT_ANALOG_JOYSTICK_Y";
                break;
            case GameManagerScript.InputType.Keyboard:
                horizontalControls = "HorizontalB";
                verticalControls = "VerticalB";
                break;
            default:
                horizontalControls = "HorizontalB";
                verticalControls = "VerticalB";
                break;
        }
    }

    void FixedUpdate()
	{
		Movement ();
    }

	void Movement () {
        Vector3 input = new Vector3(Input.GetAxisRaw(horizontalControls), 0, Input.GetAxisRaw(verticalControls));
		
		Vector3 lookdir = cam.transform.forward;
		lookdir.y = 0;
		lookdir.Normalize();
		
		Vector3 targetVelocity = Vector3.zero;
		targetVelocity += lookdir * input.z;
		targetVelocity -= cam.transform.right * -input.x;
		
		targetVelocity *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1) ? 0.7f : 1;
		targetVelocity *= speed;
		
		if (targetVelocity.magnitude > 0)
		{
			transform.rotation = Quaternion.LookRotation(new Vector3(targetVelocity.x, 0, targetVelocity.z));
		}
		// Apply a force that attempts to reach our target velocity
		Vector3 velocity = rigibody.velocity;
		Vector3 velocityChange = (targetVelocity - velocity);
		velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
		velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
		velocityChange.y = 0;
		
		rigibody.AddForce(velocityChange, ForceMode.VelocityChange);
		
		// We apply gravity manually for more tuning control
		rigibody.AddForce(new Vector3(0, -gravity * rigibody.mass, 0));
	}
    

}
