using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviourSingleton<UIManager>
{
	private void Awake() => Set(this);
	private void OnDestroy() => Release();

	[SerializeField] TransitionScreen transitionScreen;
}
