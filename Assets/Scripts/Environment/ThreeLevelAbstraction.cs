using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ThreeLevelAbstraction : MonoAction
{
	// This is for me and for me alone.
	[SerializeField] UnityEvent evt;
	[SerializeField] bool clearAfter;

	public override void PerformAction()
	{
		evt?.Invoke();
		if (clearAfter) evt.RemoveAllListeners();
	}
}
