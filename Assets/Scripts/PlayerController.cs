using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ActorBehavior))]
[RequireComponent(typeof(ActorMotor))]
public class PlayerController : MonoBehaviour
{
	private ActorMotor motor;
	private ActorBehavior actor;

	public ActorBehavior debugTargetToGrab;
	
	//Events
	void Awake()
	{
		motor = GetComponent<ActorMotor>();
		actor = GetComponent<ActorBehavior>();
	}
	
	void Update()
	{
		//While Grabbing
		if (actor.GrabbingSomeone)
		{
			WhileGrabbing();
		}

		//While Grabbed
		if (actor.BeingGrabbed)
		{
			WhileGrabbed();
		}

		//While Free
		if (!actor.PerformingAction && !actor.GrabbingSomeone && !actor.BeingGrabbed)
		{
			WhileFree();
		}

	}

	//Misc methods

	private void WhileGrabbing()
	{
		//TODO: Controls while grabbing someone else
	}

	private void WhileGrabbed()
	{
		//TODO: Controls while being grabbed
	}

	private void WhileFree()
	{
		//DEBUG: Try grabbing
		if (debugTargetToGrab != null)
		{
			actor.TryGrab(debugTargetToGrab);
		}
		
		//Relay horizontal movement input.
		float h = Input.GetAxisRaw("Horizontal");
		
		motor.LeftButton = false;
		motor.RightButton = false;
		
		if (h > 0)
		{
			motor.RightButton = true;
		}
		else if (h < 0)
		{
			motor.LeftButton = true;
		}
	}
}
