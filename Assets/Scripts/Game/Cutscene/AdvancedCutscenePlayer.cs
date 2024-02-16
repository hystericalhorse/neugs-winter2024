using System;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedCutscenePlayer : MonoBehaviour, Interactable
{
	[HideInInspector] public List<Flag> exceptions = new();

	public void Start()
	{
		lateLoaded = false;
	}

	bool lateLoaded;
	public void Update()
	{
		if (!lateLoaded)
		{
			hasPlayed = LevelManager.instance.FlagValue(this.name + "_played");
			lateLoaded = true;
		}
	}

	[Serializable]
	public class ConditionalScene
	{
		[SerializeField] public List<Flag> conditions = new();
		[SerializeField] public Cutscene cutscene;
	}

	// Returns whether or not the scene plays
	public bool PlayIf()
	{
		foreach (var condition in cutscene[currentIndex].conditions)
		{
			if (exceptions != null)
			{
				bool skip = false;
				foreach (var e in exceptions)
					if (e.name == condition.name) skip = true;
				if (skip) continue;
			}

			if (!LevelManager.instance.FlagEquals(condition.name, condition.value))
				return false;
		}

		CutsceneManager.instance.StartCutscene(cutscene[currentIndex].cutscene);
		return true;
	}

	[SerializeField] protected ConditionalScene[] cutscene;
	[SerializeField] int currentIndex = 0;
	[SerializeField] bool loopScenes = false;
	[SerializeField] bool playOnce = false;
	[SerializeField] bool hasPlayed = false;

	public void Play() => OnInteract();

	public virtual void OnInteract()
	{
		if (hasPlayed && playOnce)
		{
			return;
		}
		//if (hasPlayed == true) return;

		if (PlayIf())
		{
			if (currentIndex + 1 >= cutscene.Length)
			{
				hasPlayed = true;
				LevelManager.instance.SetFlag(this.name + "_played", hasPlayed);

				if (loopScenes)
					currentIndex = 0;
			}
			else
			{
				currentIndex += 1;
			}
		}
	}
}