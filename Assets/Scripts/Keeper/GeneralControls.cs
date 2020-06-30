using Boo.Lang;
using System;
using UnityEngine;

public class GeneralControls : MonoBehaviour
{
    public static bool isQuitting;

	private void OnApplicationQuit()
	{
		isQuitting = true;
	}

	public static void ControlQuit(in Action targetFunc) 
	{
		if (isQuitting)
		{
			return;
		}

		targetFunc();
	}
}
