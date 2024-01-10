using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
	private void Awake() => Set(this);
	private void OnDestroy() => Release();

	public void TestFunction()
	{
		Debug.LogAssertion(GameManager.instance is not null);
	}

}
