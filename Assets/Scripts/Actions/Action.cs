/*
 * Do NOT add any subclasses of this to a GameObject using the inspector.
 * Use the ActorBehavior.StartAction<T>() method instead!
 */

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ActorBehavior))]
public abstract class Action : MonoBehaviour
{
	protected ActorBehavior owner;

	//Events
	void Start ()
	{
		owner = GetComponent<ActorBehavior>();
		OnActionStart();
	}

	//Misc methods

	protected virtual void OnActionStart()
	{
	}

	protected void StopAction()
	{
		owner.StopAction();
		Destroy(this);
	}
}
