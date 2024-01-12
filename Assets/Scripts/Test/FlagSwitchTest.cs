using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagSwitchTest : FlagSwitch
{
	private void Start()
	{
		onInteractSuccess.AddListener(FlagUpdatedTest);
		onInteractFailure.AddListener(FlagDidNotUpdateTest);
	}

	private void OnDestroy()
	{
		onInteractSuccess.RemoveAllListeners();
		onInteractFailure.RemoveAllListeners();
	}

	public void FlagUpdatedTest()
	{
		Debug.Log("Flag Updated!");
	}

	public void FlagDidNotUpdateTest()
	{
		Debug.Log("Flag Not Updated!");
	}
}
