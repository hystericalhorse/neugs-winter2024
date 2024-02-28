using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{
	[SerializeField] private CutscenePlayer player;

	private void Start()
	{
		player = GetComponent<CutscenePlayer>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.transform.gameObject.GetComponent<PlayerController>())
		{
			if (player is SimpleCutscenePlayer)
				(player as SimpleCutscenePlayer).Play();

			if (player is AdvancedCutscenePlayer)
				(player as AdvancedCutscenePlayer).Play();
		}
	}
}
