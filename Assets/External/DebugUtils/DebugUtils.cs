using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DebugUtils
{
	public static void DrawRect(Vector3 min, Vector3 max, Color color)
	{
		UnityEngine.Debug.DrawLine(min, new Vector3(min.x, max.y), color);
		UnityEngine.Debug.DrawLine(new Vector3(min.x, max.y), max, color);
		UnityEngine.Debug.DrawLine(max, new Vector3(max.x, min.y), color);
		UnityEngine.Debug.DrawLine(min, new Vector3(max.x, min.y), color);
	}

	public static void DrawRect(Vector3 min, Vector3 max, Color color, float duration)
	{
		UnityEngine.Debug.DrawLine(min, new Vector3(min.x, max.y), color, duration);
		UnityEngine.Debug.DrawLine(new Vector3(min.x, max.y), max, color, duration);
		UnityEngine.Debug.DrawLine(max, new Vector3(max.x, min.y), color, duration);
		UnityEngine.Debug.DrawLine(min, new Vector3(max.x, min.y), color, duration);
	}

	public static void DrawRect(Rect rect, Color color)
	{
		DrawRect(rect.min, rect.max, color);
	}

	public static void DrawRect(Rect rect, Color color, float duration)
	{
		DrawRect(rect.min, rect.max, color, duration);
	}
}
