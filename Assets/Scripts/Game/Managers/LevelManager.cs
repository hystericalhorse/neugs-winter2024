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

	//private void OnDestroy() => Release();

	private void Start()
	{

	}

	[ContextMenu("SaveLevel")]
	public void SaveLevel()
	{
		try
		{
			LevelData lvl_dat = GetLevelData(CurrentLevel().Name);
			if (lvl_dat != null)
				LevelData[LevelData.IndexOf(lvl_dat)] = CurrentLevel().ExtractData();
			else
				LevelData.Add(CurrentLevel().ExtractData());
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

			LevelData lvl_dat = GetLevelData(CurrentLevel().Name);
			if (lvl_dat != null)
				CurrentLevel().InsertData(lvl_dat);

			// "default room" is the top room in the rooms list
			var cam = PlayerManager.instance.GetCameraController();
			if (cam != null)
			{
				cam.Center = CurrentLevel().Rooms[0].transform.position;
				cam.Limits = CurrentLevel().Rooms[0].RoomBounds;
				cam.transform.position = CurrentLevel().Rooms[0].transform.position;
			}

			PlayerManager.instance.PlacePlayerController(CurrentLevel().DefaultTransform.position);
			PlayerManager.instance.objectiveList.ClearObjectives();

			CurrentLevel().Rooms[0].OnEnterRoom();
			CurrentLevel().OnLevelLoad?.Invoke();
		}
		catch (Exception e)
		{
			Debug.LogException(e);
		}
	}

	public Level CurrentLevel()
	{
		if (currentLevel == null)
			LoadLevel();

		return currentLevel;
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
		foreach (var flag in CurrentLevel().Flags)
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
		for (var i = 0; i < CurrentLevel().Flags.Count; i++)
		{
			if (CurrentLevel().Flags[i].name == name)
			{
				CurrentLevel().Flags[i].value = value;
				return;
			}
		}

		// should only reach here if the flag doesn't exist

		CurrentLevel().Flags.Add(new(name, value));
	}
}
