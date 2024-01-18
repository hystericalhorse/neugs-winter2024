using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour, Interactable
{
	[SerializeField] private List<GameObject> lights;
	private bool toggleActive = false;

	[SerializeField] private Sprite[] switchSprites;
	[SerializeField] private SpriteRenderer sprite;

    public void OnInteract()
	{
        toggleActive = !toggleActive;
		foreach (GameObject light in lights)
		{
			light.SetActive(toggleActive);
		}

		sprite.sprite = (toggleActive ? switchSprites[0] : switchSprites[1]);
	}
}
