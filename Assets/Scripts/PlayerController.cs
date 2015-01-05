using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ActorBehavior))]
[RequireComponent(typeof(ActorMotor))]
public class PlayerController : MonoBehaviour
{
	private ActorMotor motor;
	private ActorBehavior actor;

	private ActorBehavior selectedActor = null;

	//Events
	void Awake()
	{
		motor = GetComponent<ActorMotor>();
		actor = GetComponent<ActorBehavior>();
	}
	
	void Update()
	{
		//Update GUI buttons
		UpdateGUIButtons();

		//Select the actor being clicked on
		SelectActor();

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

	void OnGrabButton()
	{
		if (selectedActor != null)
		{
			actor.TryGrab(selectedActor);
		}
		else
		{
			Debug.Log("No selected actor");
		}
	}

	void OnReleaseButton()
	{
		actor.ReleaseGrabTarget();
	}

	void OnLickVaginaButton()
	{
		actor.StartAction<LickVagina>();
	}

	//Misc methods

	private void UpdateGUIButtons()
	{
		//Enables/disables the GUI buttons

		PlayerGUI.grabButton = !actor.GrabbingSomeone && selectedActor != null && !actor.PerformingAction;
		PlayerGUI.releaseButton = actor.GrabbingSomeone && !actor.PerformingAction;

		PlayerGUI.actionButtons = actor.GrabbingSomeone && ! actor.PerformingAction;
	}

	private void SelectActor()
	{
		//Selects the actor that you click on

		if (Input.GetMouseButtonDown(0))
		{
			const float CIRCLE_RADIUS = 1f;
			
			//Get the mouse's point
			Vector3 mousePoint3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 mousePoint2D = new Vector2(mousePoint3D.x, mousePoint3D.y);
			
			//Get all objects clicked on
			Collider2D[] hits = Physics2D.OverlapCircleAll(mousePoint2D, CIRCLE_RADIUS);
			
			//If there was an actor clicked on, set it as the selected target
			foreach (Collider2D c in hits)
			{
				ActorBehavior a = c.GetComponent<ActorBehavior>();
				
				if (a != null)
				{
					selectedActor = a;
					Debug.Log("Selected an actor");
					break;
				}
			}
		}
	}
}
