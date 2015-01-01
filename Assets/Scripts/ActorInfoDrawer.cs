using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(ActorBehavior))]
public class ActorInfoDrawer : MonoBehaviour
{
	public Text textBox;
	private ActorBehavior actor;

	void Awake()
	{
		actor = GetComponent<ActorBehavior>();
	}

	void Update()
	{
		//Update the text to reflect the state of the actor
		textBox.text = "Arousal: " + actor.Arousal + "\nStimulus: " + actor.Stimulus;
	}
}
