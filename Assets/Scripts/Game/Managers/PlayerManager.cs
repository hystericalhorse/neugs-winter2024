using UnityEngine;
using System;

public class PlayerManager : MonoBehaviourSingleton<PlayerManager>
{
	[SerializeField] GameObject playerPawn;
	public PlayerController playerController;
	public CameraController cameraController;

	private void Awake()
	{
		Set(this);
	}

	private void OnDestroy() => Release();

	private void Start()
	{
#if DEBUG
		GetPlayerController();
		var camera = Camera.main;

		camera.gameObject.GetComponent<CameraController>().Reset();
#endif
	}

	public void PlacePlayerController(Vector2 position)
	{
		try
		{
			GetPlayerController().transform.position = (Vector3)position;
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

	public CameraController GetCameraController()
	{
		cameraController ??= FindAnyObjectByType<CameraController>()
			?? Camera.main.gameObject.AddComponent<CameraController>();

		return cameraController;
	}

	public void TogglePlayerController() => playerController.enabled = !playerController.enabled;
	public void TogglePlayerController(bool active) => playerController.enabled = active;
}
