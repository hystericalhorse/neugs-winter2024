using UnityEditor;
using UnityEngine;

public class AppQuit : MonoBehaviour
{
	public void QuitApplication()
	{
#if UNITY_EDITOR
		EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}
}