using UnityEngine;
using System.Collections;

public class ActorBehavior : MonoBehaviour
{
	public const float STIMULUS_DECAY_RATE = 1f;	//The rate (points per second) at which stimulus decays

	public const float MAX_AROUSAL = 100;
	public const float MAX_STIMULUS = 100;

	public float GrabReach
	{
		get { return DEFAULT_GRAB_REACH;}
	}
	
	public float Arousal { get{ return arousal;} }
	public float Stimulus { get{ return stimulus;} }

	public bool PerformingAction
	{
		get { return performingAction;}
	}

	public ActorBehavior GrabTarget
	{
		get { return grabTarget;}
	}
	public ActorBehavior Grabber
	{
		get { return grabber;}
	}
	
	public bool GrabbingSomeone
	{
		get { return GrabTarget != null;}
	}
	public bool BeingGrabbed
	{	
		get { return Grabber != null;}
	}
	
	private bool performingAction = false;

	private ActorBehavior grabTarget = null;		//The actor the we are currently grabbing
	private ActorBehavior grabber = null;			//The actor that is currently grabbing us
	
	private const float DEFAULT_GRAB_REACH = 1;		//In the future, certain things will be able to extend an actor's grab reach.  For now, it's a private constant.
	
	private float arousal;
	private float stimulus;

	//Events

	void Start()
	{
		StartAction<GropeSelf>();
	}

	void Update()
	{
		stimulus -= STIMULUS_DECAY_RATE * Time.deltaTime;

		if (stimulus < 0)
		{
			stimulus = 0;
		}
	}

	//Misc methods

	public void TryGrab(ActorBehavior target)
	{
		//Attempts to grab another actor.
		
		//Abort if trying to grab self.
		if (target == this)
		{
			return;
		}
		
		//Grab the target if she's close enough
		if (Vector3.Distance(transform.position, target.transform.position) <= GrabReach)
		{
			grabTarget = target;
			target.SetGrabber(this);
		}
	}

	public void ReleaseGrabTarget()
	{
		//Releases the currently grabbed actor
		
		if (grabTarget != null)
		{
			grabTarget.SetGrabber(null);
			grabTarget = null;
		}
	}

	public void SetGrabber(ActorBehavior grabber)
	{
		//Sets who is currently grabbing this actor.
		//DO NOT EVER CALL THIS METHOD, EXCEPT IN THE TryGrab() AND ReleaseGrabTarget() METHODS.
		
		this.grabber = grabber;
	}


	public void Arouse(float amount)
	{
		//Raises the arousal by a given amount

		arousal += amount;

		//Keep the arousal between 0 and 100
		if (arousal < 0)
		{
			arousal = 0;
		}
		else if (arousal > MAX_AROUSAL)
		{
			arousal = MAX_AROUSAL;
		}
	}

	public void Stimulate(float amount)
	{
		//Raises the target's stimulus

		//Throw an exception if the amount is negative
		if (amount < 0)
		{
			throw new NegativeStimulusException();
		}

		stimulus += amount;

		//Orgasm when stimulus is 100+
		if (stimulus >= MAX_STIMULUS)
		{
			Orgasm();
		}
	}

	public T StartAction<T>() where T : Action
	{
		//Starts performing an action.  Specify the classname of the action.  The class must extend Action.

		//Throw an error if we're already performing an action
		if (performingAction)
		{
			throw new AlreadyPerformingActionException();
		}

		T action = gameObject.AddComponent<T>();
		performingAction = true;

		return action;
	}

	public void StopAction()
	{
		//Stops the current action, if there is one.

		if (performingAction)
		{
			performingAction = false;
		}
	}

	private void Orgasm()
	{
		//TODO: Some kind of penalty for orgasming
		arousal = 0;
		stimulus = 0;

		BroadcastMessage("OnOrgasm", SendMessageOptions.DontRequireReceiver);
	}
}

public class NegativeStimulusException : System.Exception
{
	public NegativeStimulusException()
		: base("Parameter for ActorBehavior.Stimulate(float) must be positive.")
	{}
}

public class AlreadyPerformingActionException : System.Exception
{
	public AlreadyPerformingActionException()
		: base("Actor is already performing an action.  Cannot start another one.")
	{}
}
