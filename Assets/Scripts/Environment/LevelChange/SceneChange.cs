using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour, Interactable
{
    [SerializeField] public string sceneName;

    public void OnInteract()
    {
        // SceneManager.instance.onSceneLoaded += () => {
        //  PlayerManager.instance.TogglePlayerController();
        //    };

        SceneManager.instance.LoadScene(sceneName);
    }

    public void OnApplicationQuit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();

    }
}
