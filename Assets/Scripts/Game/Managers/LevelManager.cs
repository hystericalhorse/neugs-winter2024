using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviourSingleton<LevelManager>
{
	private Level currentLevel;
	public List<LevelData> LevelData = new();

	private void Awake()
	{
		Set(this);
	}

	private void OnDestroy() => Release();

	private void Start()
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

			// "default room" is the top room in the rooms list
			PlayerManager.instance.GetCameraController().Center = currentLevel.Rooms[0].transform.position;
			PlayerManager.instance.GetCameraController().Limits = currentLevel.Rooms[0].RoomBounds;
			PlayerManager.instance.GetCameraController().transform.position = currentLevel.Rooms[0].transform.position;

			PlayerManager.instance.PlacePlayerController(currentLevel.DefaultTransform.position);

			currentLevel.Rooms[0].OnEnterRoom();
			currentLevel.OnLevelLoad?.Invoke();
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

	/// <summary>
	/// Checks if the value of a flag is the same as another, without updating the flag.
	/// </summary>
	/// <param name="name">name of the flag</param>
	/// <param name="value"></param>
	/// <returns>Returns true if the value matches, else returns false.</returns>
	public bool FlagEquals(string name, bool value)
	{
		var flag = GetFlag(name);
		return (flag != null && flag.value == value);
	}

	public bool FlagValue(string name)
	{
		var flag = GetFlag(name);
		if (flag != null) return flag.value;

		return false;
	}

	public void SetFlag(string name, bool value)
	{
		for (var i = 0; i < currentLevel.Flags.Count; i++)
		{
			if (currentLevel.Flags[i].name == name)
			{
				currentLevel.Flags[i].value = value;
				return;
			}
		}

		// should only reach here if the flag doesn't exist

		currentLevel.Flags.Add(new(name, value));
	}
}
