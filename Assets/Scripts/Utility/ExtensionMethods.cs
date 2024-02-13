using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class ExtensionMethods
{
	public static void RecursiveSetActive(this GameObject obj, bool state)
	{
		foreach (Transform child in obj.transform)
		{
			child.gameObject.RecursiveSetActive(state);
		}

		obj.SetActive(state);
	}

	public static T GetRandom<T>(this T[] array)
	{
		var i = Helpers.random.Next(0, (array.Length - 1));
		return array[i];
	}

	public static T GetRandom<T>(this List<T> array)
	{
		var i = Helpers.random.Next(0, array.Count);
		return array[i];
	}

	public static Vector2 UnNormalize(this Vector2 vec2)
	{
		Vector2 _ = new(0,0);

		if (vec2.x != 0)
			_.x = (vec2.x > 0) ? 1 : -1;

		if (vec2.y != 0)
			_.y = (vec2.y > 0) ? 1 : -1;

		return _;
	}

	public enum Axis { Horizontal, Vertical }
	/// <param name="favoriteAxis">Which axis to favor if the x and y values are equal</param>
	/// <returns>A Vector2 snapped to a cardinal direction</returns>
	public static Vector2 Cardinalize(this Vector2 vec2, Axis favoriteAxis = Axis.Vertical)
	{
		Vector2 _ = new(0,0);

		switch (favoriteAxis)
		{
			default:
			case Axis.Vertical:
				if (Mathf.Abs(vec2.x) > Mathf.Abs(vec2.y))
					_ = new(vec2.UnNormalize().x, 0);
				else
					_ = new(0, vec2.UnNormalize().y);
				break;
			case Axis.Horizontal:
				if (Mathf.Abs(vec2.x) >= Mathf.Abs(vec2.y))
					_ = new(vec2.UnNormalize().x, 0);
				else
					_ = new(0, vec2.UnNormalize().y);
				break;
		}

		return _;

	}

}