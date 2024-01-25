using UnityEngine;

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
	[SerializeField] float zoom; //TODO Implementation

	private void Awake()
	{
		
		Camera = GetComponent<Camera>();
		if (TargetTransform == null) TargetTransform = GameObject.FindGameObjectWithTag("Player").transform;
		transform.position = TargetTransform.position;
	}

	private void Update()
	{
		pos = TargetTransform.position;

		pos.z = -10; // z-value doesn't particularly matter if the camera is orthographic.

		// prevents the camera from moving past world limits. this only works well for rectangular maps.
		// https://forum.unity.com/threads/2d-top-down-camera-edge.233036/
		pos.x = Mathf.Clamp(pos.x, Center.x - Limits.x * 0.5f, Center.x + Limits.x * 0.5f);
		pos.y = Mathf.Clamp(pos.y, Center.y - Limits.y * 0.5f, Center.y + Limits.y * 0.5f);

		if (fixedCamera)
			transform.position = pos;
	}

	private void FixedUpdate()
	{	
		if (!fixedCamera)
			transform.position = Vector3.Lerp(transform.position, pos, followSpeed * Time.smoothDeltaTime); // smooth camera motion
	}

	public void NoLerpResetPosition()
	{
		transform.position = TargetTransform.position;
	}
}
