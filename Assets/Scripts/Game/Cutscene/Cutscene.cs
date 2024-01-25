using System.Collections.Generic;
using UnityEngine;
using System;
using JetBrains.Annotations;

[Serializable]
public class Cutscene : Timeline<Shot>
{

}

[Serializable]
public class Shot
{
	//public string shotName = String.Empty;

	public Sprite shotImage = null;
	public Sprite leftCharacterImage = null;
	public Sprite rightCharacterImage = null;

	public List<string> shotScript = new();
	public string narrator;
}

[Serializable]
public class Dialogue
{
	public string Speaker;
	public List<string> Script;
}

[Serializable]
// TBI
public class NuSprite
{
	public string ID;
	public Sprite Sprite;
}
