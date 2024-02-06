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
}