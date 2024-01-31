using System;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
	[SerializeField] public string Name = string.Empty;
	[SerializeField] public List<Flag> Flags = new();
	[SerializeField] public List<Room> Rooms = new();
	[SerializeField] public Transform DefaultTransform;

	private void Start()
	{
		// M.A.D.
		if (FindObjectOfType<Level>() != this) Destroy(gameObject);
		AudioManager.instance.PlaySound("HouseAmbience");

		//Rooms.AddRange(FindObjectsOfType<Room>(true));

		if (DefaultTransform == null) DefaultTransform = transform;
	}

	public LevelData ExtractData()
	{
		return new LevelData(Name, Flags);
	}

	public void InsertData(LevelData data)
	{
		if (data == null) return;

		Name = data.Name;
		Flags = data.Flags;
	}
}

[Serializable]
public class LevelData
{
	public LevelData(string name, List<Flag> flags)
	{
		Name = name; Flags = flags;
	}
	[SerializeField] public string Name = string.Empty;
	[SerializeField] public List<Flag> Flags = new();
}

[Serializable]
public class Flag
{ [SerializeField] public string name; [SerializeField] public bool value; }
