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

	public IEnumerator WaitForSceneLoad(UnityEngine.AsyncOperation operation, bool fadeIn = false)
	{
		sceneLoaded = false;

		while (!operation.isDone)
		{
			loadingPercent = Mathf.Clamp01(operation.progress) * 100;
			//Debug.Log($"Loading: {loadingPercent}%");
			yield return null;
		}

		sceneLoaded = true;

		if (onSceneLoaded != null) onSceneLoaded();
		onSceneLoaded = null;

		if (fadeIn)
			StartCoroutine(FindObjectOfType<TransitionScreen>().FadeOut());
	}

	public void LoadScene(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single, bool fadeIn = false)
	{
		StartCoroutine(WaitForSceneLoad(UnitySceneManager.LoadSceneAsync(sceneName, loadSceneMode), fadeIn));
	}

	public void UnloadScene(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
	{
		StartCoroutine(WaitForSceneLoad(UnitySceneManager.UnloadSceneAsync(sceneName)));
	}
}
