using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
	public Transform TargetTransform;
    public Camera Camera;

	public Vector2 Limits = Vector2.positiveInfinity;
	public Vector2 Center = Vector2.zero;
	Vector3 pos = Vector3.zero;

	[SerializeField] public bool fixedCamera = false;
	[SerializeField,Range(1,10)] float followSpeed = 10;
	[Range(0, 4)] float zoom = 1;
	[SerializeField, Range(0, 4)] public float targetZoom;
	public float zoomSpeed = 0.1f;

	private PixelPerfectCamera ppc;

	private bool paused;

	private void Awake()
	{
		Camera = GetComponent<Camera>();
		ppc ??= GetComponent<PixelPerfectCamera>();
	}

	private void Start()
	{
		
	}

	private void Update()
	{
		if (paused) return;

		pos = TargetTransform.position;

		pos.z = -10; // z-value doesn't particularly matter if the camera is orthographic.

		// prevents the camera from moving past world limits. this only works well for rectangular maps.
		// https://forum.unity.com/threads/2d-top-down-camera-edge.233036/
		pos.x = Mathf.Clamp(pos.x, Center.x - Limits.x * 0.5f, Center.x + Limits.x * 0.5f);
		pos.y = Mathf.Clamp(pos.y, Center.y - Limits.y * 0.5f, Center.y + Limits.y * 0.5f);

		if (fixedCamera)
			transform.position = pos;

		if (zoom != targetZoom)
		{
			zoom = Mathf.Lerp(zoom, targetZoom, Time.deltaTime * zoomSpeed);
			ppc.assetsPPU = (int) (zoom * 64);
		}
	}

	private void FixedUpdate()
	{
		if (paused) return;

		if (!fixedCamera)
			transform.position = Vector3.Lerp(transform.position, pos, followSpeed * Time.smoothDeltaTime); // smooth camera motion
	}

	public void NoLerpResetPosition()
	{
		if (TargetTransform == null)
		{
			pos = Center;
			transform.position = Center;
		}
		else
		{
			pos = TargetTransform.position;

			pos.z = -10;

			if (Limits.x == 0) pos.x = Center.x;
			else pos.x = Mathf.Clamp(pos.x, Center.x - Limits.x * 0.5f, Center.x + Limits.x * 0.5f);

			if (Limits.y == 0) pos.y = Center.y;
			else pos.y = Mathf.Clamp(pos.y, Center.y - Limits.y * 0.5f, Center.y + Limits.y * 0.5f);

			if (!paused) transform.position = pos;
		}
	}

	public void ResetController()
	{
		TargetTransform = PlayerManager.instance.GetPlayerController().transform;
		NoLerpResetPosition();
	}

	public void ResetController(Transform t)
	{
		TargetTransform = t; //PlayerManager.instance.GetPlayerController().transform;
		NoLerpResetPosition();
	}

	public void Pause() => paused = true;
	public void Unpause() => paused = false;
}
