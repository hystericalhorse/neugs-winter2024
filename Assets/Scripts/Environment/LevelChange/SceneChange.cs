using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneChange : MonoBehaviour, Interactable
{
    [SerializeField] public string sceneName;
    [SerializeField] private SceneManager sceneManager;

    public void OnInteract()
    {
        // SceneManager.instance.onSceneLoaded += () => {
        // PlayerManager.instance.TogglePlayerController();
        //  };
        FindObjectOfType<TransitionScreen>().onTransitionEnd.AddListener(() => { SceneManager.instance.LoadScene(sceneName); });
        FindObjectOfType<TransitionScreen>().Transition(0);

	}

    public void TitleScreenChange()
    {
        sceneManager.LoadScene(sceneName);
        
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
