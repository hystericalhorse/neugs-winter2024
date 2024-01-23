using UnityEngine;
using System;

public class PlayerManager : MonoBehaviourSingleton<PlayerManager>
{
	[SerializeField] GameObject playerPawn;
	public PlayerController playerController;

	private void Awake() => Set(this);
	private void OnDestroy() => Release();

	public void PlacePlayerController(Vector2 position)
	{
		try
		{
			playerController.transform.position = (Vector3)position;
		}
		catch (Exception e)
		{
			Debug.LogException(e);
		}
	}

	public PlayerController GetPlayerController()
	{
		playerController ??= FindAnyObjectByType<PlayerController>()
			?? Instantiate(playerPawn).GetComponent<PlayerController>();

		return playerController;
	}

	public void TogglePlayerController()
	{
		playerController.enabled = !playerController.enabled;
	}
}
