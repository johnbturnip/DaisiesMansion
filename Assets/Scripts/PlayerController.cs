using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ActorBehavior))]
[RequireComponent(typeof(ActorMotor))]
public class PlayerController : MonoBehaviour
{
	private ActorMotor motor;

	//Events
	void Awake()
	{
		motor = GetComponent<ActorMotor>();
	}
	
	void Update()
	{
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
