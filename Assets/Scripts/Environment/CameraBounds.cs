using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider2D))]
// Sets new CameraBounds when the player walks into it.
public class CameraBounds : MonoBehaviour
{
	[SerializeField] Color boundsCrossColor = Color.red;

	private Vector2 center;
	public Vector2 bounds = Vector2.positiveInfinity;

	private Vector2 cachedCenter;
	private Vector2 cachedBounds;

	public void Start()
	{
		center = transform.position;
		GetComponent<BoxCollider2D>().isTrigger = true;
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.transform.GetComponent<PlayerController>()) return;

		var cam = PlayerManager.instance.GetCameraController();

		cachedCenter = cam.Center;
		cachedBounds = cam.Limits;

		cam.Center = center;
		cam.Limits = bounds;
	}

	public void OnTriggerExit2D(Collider2D collision)
	{
		if (!collision.transform.GetComponent<PlayerController>()) return;

		var cam = PlayerManager.instance.GetCameraController();

		cam.Center = cachedCenter;
		cam.Limits = cachedBounds;

		cachedCenter = default;
		cachedBounds = default;
	}

#if UNITY_EDITOR
	void OnEnable()
	{
		SceneView.duringSceneGui += OnScene;
	}

	void OnDisable()
	{
		SceneView.duringSceneGui -= OnScene;
	}

	private void OnScene(SceneView scene)
	{
		center = transform.position;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = boundsCrossColor;

		var halfBounds = bounds * 0.5f;

		Vector2 tL = new(center.x - halfBounds.x, center.y - halfBounds.y);
		Vector2 bL = new(center.x - halfBounds.x, center.y + halfBounds.y);
		Vector2 tR = new(center.x + halfBounds.x, center.y - halfBounds.y);
		Vector2 bR = new(center.x + halfBounds.x, center.y + halfBounds.y);

		Gizmos.DrawLine(tL, tR);
		Gizmos.DrawLine(tR, bR);
		Gizmos.DrawLine(bR, bL);
		Gizmos.DrawLine(bL, tL);

		Gizmos.DrawLine(center - (bounds * 0.5f), center + (bounds * 0.5f));
	}
#endif
}
