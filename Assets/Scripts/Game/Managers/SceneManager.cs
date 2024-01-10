using UnityEngine.SceneManagement;
using UnityEngine;

using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;
using System.Collections;

public class SceneManager : MonoBehaviourSingleton<SceneManager>
{
	public bool sceneLoaded;
	public float loadingPercent;
	public delegate void OnSceneLoaded();
	public OnSceneLoaded onSceneLoaded;

	private void Awake() => Set(this);
	private void OnDestroy() => Release();

	public IEnumerator WaitForSceneLoad(UnityEngine.AsyncOperation operation, bool fade = false)
	{
		sceneLoaded = false;

		while (!operation.isDone)
		{
			loadingPercent = Mathf.Clamp01(operation.progress) * 100;
			//Debug.Log($"Loading: {loadingPercent}%");
			yield return null;
		}

		sceneLoaded = true;

		if (onSceneLoaded is not null) onSceneLoaded();
		onSceneLoaded = null;
	}

	public void LoadScene(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single, bool fade = false)
	{
		StartCoroutine(WaitForSceneLoad(UnitySceneManager.LoadSceneAsync(sceneName, loadSceneMode), fade));
	}

	public void UnloadScene(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
	{
		StartCoroutine(WaitForSceneLoad(UnitySceneManager.UnloadSceneAsync(sceneName)));
	}
}
