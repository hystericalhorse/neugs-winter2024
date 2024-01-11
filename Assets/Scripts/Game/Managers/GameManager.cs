using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
	SessionData gameData;

	private void Awake() => Set(this);
	private void OnDestroy() => Release();

	private void Start() => gameData = new();

	[ContextMenu("Save")]
	public void SaveSession()
	{
		gameData.SessionName = "test_session";
		LevelManager.instance.SaveLevel();//normally, this would occur when a level is switched.
		gameData.Levels = LevelManager.instance.LevelData;

		SessionHandler sh = new();
		sh.StoreSession(gameData.SessionName, gameData);
	}

	[ContextMenu("Load")]
	public void LoadSession()
	{
		SessionHandler sh = new();
		gameData = sh.RetrieveSession("test_session");

		LevelManager.instance.LevelData = gameData.Levels;
		LevelManager.instance.LoadLevel();
	}
	//public void LoadSession(string name)
	//{
	//	SessionHandler sh = new();
	//	gameData = sh.RetrieveSession(name);
	//}


}
