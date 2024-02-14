using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System;

[Serializable]
public class Cutscene : Timeline<Shot> { }

[Serializable]
public class Shot
{
	//public string shotName = String.Empty;

	public Sprite shotImage = null;
	public Sprite leftCharacterImage = null;
	public Sprite rightCharacterImage = null;

	public List<string> shotScript = new();
	public string narrator = string.Empty;
	public bool silent = false;
	public string[] textSounds = { };

	public UnityEvent onShotBegin;
	public UnityEvent onShotEnd;
}