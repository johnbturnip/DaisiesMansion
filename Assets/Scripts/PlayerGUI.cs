using UnityEngine;
using System.Collections;

public class PlayerGUI : MonoBehaviour
{
	public static bool grabButton = false;
	public static bool releaseButton = false;

	public static bool actionButtons = false;

	//Events

	void OnGUI()
	{
		//Grab button
		if (grabButton && GUILayout.Button("Grab"))
		{
			BroadcastMessage("OnGrabButton");
		}

		//Release button
		if (releaseButton && GUILayout.Button("Release"))
		{
			BroadcastMessage("OnReleaseButton");
		}

		//Action buttons
		if (actionButtons)
		{
			if (GUILayout.Button("Lick Vagina"))
			{
				BroadcastMessage("OnLickVaginaButton");
			}
		}
	}
}
