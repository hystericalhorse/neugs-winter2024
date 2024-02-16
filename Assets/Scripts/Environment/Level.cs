using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour
{
	[SerializeField] public string Name = string.Empty;
	[SerializeField] public List<Flag> Flags = new();
	[SerializeField] public List<Room> Rooms = new();
	[SerializeField] public Transform DefaultTransform;
	[SerializeField] public UnityEvent OnLevelLoad;

	private void Awake()
	{
		// M.A.D.
		if (FindObjectOfType<Level>() != this) Destroy(gameObject);

		if (DefaultTransform == null) DefaultTransform = transform;
	}

	private void Start()
	{
		LevelManager.instance.LoadLevel();
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
{
	public Flag(string name, bool value)
	{
		this.name = name;
		this.value = value;
	}

	[SerializeField] public string name;
	[SerializeField] public bool value;
}
