using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneChange : MonoBehaviour, Interactable
{
    [SerializeField] public string sceneName;

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
