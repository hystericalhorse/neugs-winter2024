using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour, Interactable
{
	[SerializeField] private List<GameObject> lights;
	private bool toggleActive = false;

    public void OnInteract()
	{
        toggleActive = !toggleActive;
		foreach (GameObject light in lights)
		{
			light.SetActive(toggleActive);
		}
	}
}
