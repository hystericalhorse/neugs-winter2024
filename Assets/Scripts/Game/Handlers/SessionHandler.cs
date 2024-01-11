using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

// With Apologies to LlamAcademy @ https://www.youtube.com/@LlamAcademy
/// <summary>DI Handler for Managing Serialization and General Session Data.</summary>
public class SessionHandler
{
	DataService ds = new JSONSerializer();

	//C:\Users\{user}\AppData\LocalLow\DefaultCompany\Lightgone
	public bool StoreSession(string sessionName, SessionData data) => ds.SaveData(sessionName, data, true);
	public SessionData RetrieveSession(string sessionName) => ds.LoadData<SessionData>(sessionName, true);

	public string[] GetStoredSessions() { return new string[] { }; }//TODO
}

[Serializable]
public class SessionData // Relevant Game Data
{
	public string SessionName;
	[SerializeField] public List<LevelData> Levels;
}

public class JSONSerializer : DataService
{
	public bool SaveData<T>(string relativePath, T data, bool encrypted)
	{
		string path = Application.persistentDataPath + "\\" + relativePath;
		try
		{
			if (File.Exists(path))
				Debug.Log("Found data. Overwriting.");
			else
				Debug.Log("Writing data.");

			FileStream stream = File.Create(path);
			stream.Close();

			File.WriteAllText(path, JsonConvert.SerializeObject(data));

			return true;
		}
		catch (Exception e)
		{
			Debug.LogError($"Unable to save data: {e.Message}\n{e.StackTrace}");
			return false;
		}
	}

	public T LoadData<T>(string relativePath, bool encrypted)
	{
		string path = Application.persistentDataPath + "\\" + relativePath;
		if (!File.Exists(path))
		{
			Debug.LogError($"Unable to retrieve file at {path}. File does not exist.");
			throw new FileNotFoundException($"{path} does not exist.");
		}

		try
		{

			T data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
			return data;
		}
		catch (Exception e)
		{
			Debug.LogError($"Unable to retrieve data: {e.Message}\n{e.StackTrace}");
			throw e;
		}
	}
}


public interface DataService
{
	public bool SaveData<T>(string relativePath, T data, bool encrypted);
	public T LoadData<T>(string relativePath, bool encrypted);
}
