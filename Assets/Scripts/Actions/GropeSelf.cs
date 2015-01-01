using UnityEngine;
using System.Collections;

public class GropeSelf : Action
{
	private float arousalToGive = 2f;
	private float arousalGiven = 0f;

	private float duration = 2f;
	private float timer = 0f;

	void FixedUpdate()
	{
		//Slowly arouse self

		float arousalRate = arousalToGive / duration;
		float arousalToAddThisFrame = arousalRate * Time.fixedDeltaTime;

		//Make sure the action does not give more arousal than intended.
		if (arousalGiven + arousalToAddThisFrame > arousalToGive)
		{
			arousalToAddThisFrame = arousalToGive - arousalGiven;
		}

		//Add the arousal
		owner.Arouse(arousalToAddThisFrame);
		arousalGiven += arousalToAddThisFrame;

		//Stop when time is up
		timer += Time.fixedDeltaTime;
		if (timer >= duration)
		{
			StopAction();
		}
	}
}
