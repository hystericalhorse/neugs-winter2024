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

	public delegate void OnMoveDone();
	public void MovePlayerController(Vector2 move, OnMoveDone onMoveDone = null)
	{
		try
		{
			StartCoroutine(GetPlayerController().MovePawn(move, () => { onMoveDone?.Invoke(); }));
		}
		catch (Exception e)
		{
			Debug.LogException(e);
		}
	}
	public void MoveCameraController(Transform target, OnMoveDone onMoveDone = null)
	{
		try
		{
			StartCoroutine(GetCameraController().MoveCamera(target, () => { onMoveDone?.Invoke(); }));
		}
		catch (Exception e)
		{
			Debug.LogException(e);
		}
	}
	public void MoveCameraControllerForSeconds(Transform target, float forSeconds, OnMoveDone onMoveDone = null)
	{
		try
		{
			StartCoroutine(GetCameraController().MoveCamera(target, forSeconds, () => { onMoveDone?.Invoke(); }));
		}
		catch (Exception e)
		{
			Debug.LogException(e);
		}
	}

	public PlayerController GetPlayerController()
	{
		if (playerController == null)
		{
			playerController = FindAnyObjectByType<PlayerController>() ?? Instantiate(playerPrefab).GetComponent<PlayerController>();
		}

		return playerController;
	}

	public CameraController GetCameraController()
	{
		if (cameraController == null)
		{
			//cameraController = FindAnyObjectByType<Camera>().gameObject.AddComponent<CameraController>()
			//?? Camera.main?.gameObject.AddComponent<CameraController>()
			//?? FindAnyObjectByType<CameraController>()
			//?? Instantiate(cameraPrefab).GetComponent<CameraController>();

			cameraController = Instantiate(cameraPrefab).GetComponent<CameraController>();
			DontDestroyOnLoad(cameraController);
		}

		return cameraController;
	}

	public void TogglePlayerController() => playerController.enabled = !playerController.enabled;
	public void TogglePlayerController(bool active) => playerController.enabled = active;
}