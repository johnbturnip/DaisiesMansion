using UnityEngine;
using System.Collections;

public class ActorBehavior : MonoBehaviour
{
	public const float STIMULUS_DECAY_RATE = 1f;	//The rate (points per second) at which stimulus decays

	public enum State {Free, Grabbing, PerformingAction}

	private State currentState = State.Free;

	public float Arousal { get{ return arousal;} }
	public float Stimulus { get{ return stimulus;} }

	private float arousal;
	private float stimulus;

	//Events

	void Start()
	{
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

	public void Arouse(float amount)
	{
		//Raises the arousal by a given amount

		arousal += amount;

		//Keep the arousal between 0 and 100
		if (arousal < 0)
		{
			arousal = 0;
		}
		else if (arousal > 100)
		{
			arousal = 100;
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
		if (stimulus >= 100)
		{
			Orgasm();
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
	{
	}
}
