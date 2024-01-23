using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour, Interactable
{
	[SerializeField] private List<GameObject> lights;
	private bool toggleActive = false;

	[SerializeField] private Sprite[] switchSprites;
	private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

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
