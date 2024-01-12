using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviourSingleton<PlayerManager>
{
	private void Awake() => Set(this);
	private void OnDestroy() => Release();

}
