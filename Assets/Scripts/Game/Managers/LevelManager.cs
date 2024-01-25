using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviourSingleton<LevelManager>
{
	private Level currentLevel;
	public List<LevelData> LevelData = new();

	private void Awake() => Set(this);
	private void OnDestroy() => Release();

#if DEBUG
	private void Start()
	{
		LoadLevel();
	}
#endif

	public void LevelsInit()
	{

	}

	[ContextMenu("SaveLevel")]
	public void SaveLevel()
	{
		try
		{
			LevelData lvl_dat = GetLevelData(currentLevel.Name);
			if (lvl_dat != null)
				LevelData[LevelData.IndexOf(lvl_dat)] = currentLevel.ExtractData();
			else
				LevelData.Add(currentLevel.ExtractData());
		}
		catch (Exception e)
		{
			Debug.LogException(e);
		}
	}

	[ContextMenu("LoadLevel")]
	public void LoadLevel()
	{
		try
		{
			currentLevel = FindObjectOfType<Level>();

			LevelData lvl_dat = GetLevelData(currentLevel.Name);
			if (lvl_dat != null)
				currentLevel.InsertData(lvl_dat);

			currentLevel.Rooms[0].OnEnterRoom();
		}
		catch (Exception e)
		{
			Debug.LogException(e);
		}
		
	}

	LevelData GetLevelData(string name)
	{
		foreach (var lvl_dat in LevelData)
		{
			if (lvl_dat.Name == name) return lvl_dat;
		}

		return null;
	}

	Flag GetFlag(string name)
	{
		foreach (var flag in currentLevel.Flags)
		{
			if (flag.name == name) return flag;
		}

		return null;
	}

	/// <summary>
	/// Update a flag in the current Level.
	/// </summary>
	/// <param name="name">The name of the flag.</param>
	/// <param name="value">The desired value to set the flag to.</param>
	/// <returns>Whether or not the level was updated.</returns>
	public bool UpdateLevel(string name, bool value)
	{
		var flag = GetFlag(name);
		if (flag != null && flag.value != value)
		{
			if (flag.value != value)
			{
				flag.value = value;
				return true;
			}
		}

		return false;
	}
}
