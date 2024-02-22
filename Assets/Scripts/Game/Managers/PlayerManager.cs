using UnityEngine;
using System;

public class PlayerManager : MonoBehaviourSingleton<PlayerManager>
{
	[SerializeField] GameObject playerPrefab;
	[SerializeField] GameObject cameraPrefab;
	private PlayerController playerController;
	private CameraController cameraController;
	public ObjectiveList objectiveList;

	private void Awake()
	{
		Set(this);
	}

	private void OnDestroy() => Release();

	private void Start()
	{
		
	}

	public void PlacePlayerController(Vector2 position)
	{
		try
		{
			GetPlayerController().transform.position = (Vector3)position;
			GetCameraController().NoLerpResetPosition();
		}
		catch (Exception e)
		{
			Debug.LogException(e);
		}
	}

	public PlayerController GetPlayerController()
	{
		playerController ??= FindAnyObjectByType<PlayerController>()
			?? Instantiate(playerPrefab).GetComponent<PlayerController>();

		return playerController;
	}

	public CameraController GetCameraController()
	{
		cameraController ??= FindAnyObjectByType<CameraController>()
			?? Instantiate(cameraPrefab).GetComponent<CameraController>();

		return cameraController;
	}

	public void TogglePlayerController() => playerController.enabled = !playerController.enabled;
	public void TogglePlayerController(bool active) => playerController.enabled = active;
}