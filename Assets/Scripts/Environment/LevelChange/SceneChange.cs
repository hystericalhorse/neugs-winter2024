using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour, Interactable
{
    [SerializeField] public string sceneName;
    private Scene scene;
    public void OnInteract()
    {
        FindObjectOfType<TransitionScreen>().afterFadeIn.AddListener(() => {
			SceneManager.instance.LoadScene(sceneName, fadeIn: true);
        });

        FindObjectOfType<TransitionScreen>().Transition(0, false);
	}



    public void LoadScene()
    {
        SceneManager.instance.LoadScene(sceneName);
    }
}
