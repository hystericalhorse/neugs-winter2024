using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
	public Transform TargetTransform;
    public Camera Camera;

	public Vector2 Limits = Vector2.positiveInfinity;
	Vector3 pos = Vector3.zero;

	[SerializeField] bool fixedCamera = false;
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
		pos.x = Mathf.Clamp(pos.x, -Limits.x, Limits.x);
		pos.y = Mathf.Clamp(pos.y, -Limits.y, Limits.y);

		if (fixedCamera)
			transform.position = pos;
	}

	private void FixedUpdate()
	{	
		if (!fixedCamera)
			transform.position = Vector3.Lerp(transform.position, pos, followSpeed * Time.smoothDeltaTime); // smooth camera motion
	}

	public void Teleport()
	{
		transform.position = TargetTransform.position;
	}
}
