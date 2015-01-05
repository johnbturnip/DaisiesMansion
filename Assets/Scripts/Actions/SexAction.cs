using UnityEngine;
using System.Collections;

public abstract class SexAction : Action
{
	protected float duration = 0f;

	protected float totalTargetStimulus = 0f;
	protected float totalOwnerStimulus = 0f;

	protected float totalTargetArousal = 0f;
	protected float totalOwnerArousal = 0f;
	
	private float timer = 0f;

	private float targetStimulusGiven = 0f;
	private float ownerStimulusGiven = 0f;

	private float targetArousalGiven = 0f;
	private float ownerArousalGiven = 0f;

	private ActorBehavior target
	{
		get { return owner.GrabTarget;}
	}

	//Events

	void FixedUpdate()
	{
		ArouseTarget();
		ArouseOwner();
		StimuluateTarget();
		StimulateOwner();

		//Stop when time is up
		timer += Time.fixedDeltaTime;
		if (timer >= duration)
		{
			StopAction();
		}
	}

	private void ArouseTarget()
	{
		float arousalRate = totalTargetArousal / duration;
		float arousalToAddThisFrame = arousalRate * Time.fixedDeltaTime;
		
		//Make sure the action does not give more arousal than intended.
		if (targetArousalGiven + arousalToAddThisFrame > totalTargetArousal)
		{
			arousalToAddThisFrame = totalTargetArousal - targetArousalGiven;
		}
		
		//Add the arousal
		target.Arouse(arousalToAddThisFrame);
		targetArousalGiven += arousalToAddThisFrame;
	}

	private void ArouseOwner()
	{
		float arousalRate = totalOwnerArousal / duration;
		float arousalToAddThisFrame = arousalRate * Time.fixedDeltaTime;
		
		//Make sure the action does not give more arousal than intended.
		if (ownerArousalGiven + arousalToAddThisFrame > totalOwnerArousal)
		{
			arousalToAddThisFrame = totalOwnerArousal - ownerArousalGiven;
		}
		
		//Add the arousal
		owner.Arouse(arousalToAddThisFrame);
		ownerArousalGiven += arousalToAddThisFrame;
	}

	private void StimuluateTarget()
	{
		float stimulusRate = totalTargetStimulus / duration;
		float stimulusToAddThisFrame = stimulusRate * Time.fixedDeltaTime;
		
		//Make sure the action does not give more stimulus than intended.
		if (targetStimulusGiven + stimulusToAddThisFrame > totalTargetStimulus)
		{
			stimulusToAddThisFrame = totalTargetStimulus - targetStimulusGiven;
		}
		
		//Add the stimulus
		target.Stimulate(stimulusToAddThisFrame);
		targetStimulusGiven += stimulusToAddThisFrame;
	}

	private void StimulateOwner()
	{
		float stimulusRate = totalOwnerStimulus / duration;
		float stimulusToAddThisFrame = stimulusRate * Time.fixedDeltaTime;
		
		//Make sure the action does not give more stimulus than intended.
		if (ownerStimulusGiven + stimulusToAddThisFrame > totalOwnerStimulus)
		{
			stimulusToAddThisFrame = totalOwnerStimulus - ownerStimulusGiven;
		}
		
		//Add the stimulus
		owner.Stimulate(stimulusToAddThisFrame);
		ownerStimulusGiven += stimulusToAddThisFrame;
	}

}
