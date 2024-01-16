using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Dialogue
{
	public string ActorName;
	public string[] Lines;
}

public class DialogueManager : MonoBehaviourSingleton<DialogueManager>
{
	public bool isRunning = false;
	public Image imageCutscene;
	void Awake() => Set(this);
	void OnDestroy() => Release();

	private void Update()
	{
		
	}

	Queue<Dialogue> script = new();

	public Dialogue currentDialogue;
	public int line;
	public string display;

	public void AddDialogue(Dialogue[] dlogs)
	{
		foreach (var dialog in dlogs)
			script.Enqueue(dialog);
	}

	public void NextLine()
	{
		if (!isRunning) return;
        imageCutscene.enabled = true;
        if (line >= currentDialogue.Lines.Length - 1)
		{
			if (!script.TryDequeue(out currentDialogue))
			{
				StopDialogue();
				return;
			}

			line = 0;
		}
		else
		{
			line++;
		}

		display = currentDialogue.ActorName + ": " + currentDialogue.Lines[line];
		Debug.Log(display);
	}

	public void PlayDialogue()
	{
		
		isRunning = true;
		if (currentDialogue is null) currentDialogue = script.Peek();
		NextLine();
	}

	public void StopDialogue()
	{
		imageCutscene.enabled = false;
		isRunning = false;

		currentDialogue = null;
	}
}

