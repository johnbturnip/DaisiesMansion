using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ActorBehavior))]
[RequireComponent(typeof(Rigidbody2D))]

public class ActorMotor : MonoBehaviour
{
	public const float MAX_WALK_SPEED = 10f;
	public const float MIN_WALK_SPEED = 1f;
	
	public float WalkingSpeed
	{
		get
		{
			return Mathf.Lerp(MAX_WALK_SPEED, MIN_WALK_SPEED, actor.Arousal / ActorBehavior.MAX_AROUSAL);
		}
	}

	public float JumpingSpeed
	{
		get { return JUMP_SPEED;}
	}

	private const float JUMP_SPEED = 1;		//In the future, I may want to dynamically calculate the speed based on arousal, breast size, etc.

	public bool LeftButton
	{
		get { return leftButton;}
		set { leftButton = value;}
	}

	public bool RightButton
	{
		get { return rightButton;}
		set { rightButton = value;}
	}

	private bool leftButton = false;
	private bool rightButton = false;
	
	private ActorBehavior actor;

	//Events
	void Awake()
	{
		actor = GetComponent<ActorBehavior>();
	}

	void Update()
	{
		WalkingControls();
	}

	//Misc methods

	public void Jump()
	{
		//makes the character jump if on the ground.
		
	}

	private void WalkingControls()
	{
		Vector2 velocity = rigidbody2D.velocity;

		if (rightButton)
		{
			velocity.x = WalkingSpeed;
		}
		else if (leftButton)
		{
			velocity.x = WalkingSpeed * -1;
		}
		else
		{
			velocity.x = 0;
		}

		rigidbody2D.velocity = velocity;
	}
}
