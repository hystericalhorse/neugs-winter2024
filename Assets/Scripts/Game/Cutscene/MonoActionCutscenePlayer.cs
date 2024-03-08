using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoActionCutscenePlayer : MonoAction
{
    [SerializeField] GameObject player;

	private void Start()
	{
		if (player.GetComponent<CutscenePlayer>() == null) Destroy(player);
	}

	public override void PerformAction() => player.GetComponent<CutscenePlayer>().Play();
}
