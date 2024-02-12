using System.Collections;
using System.Collections.Generic;
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
}