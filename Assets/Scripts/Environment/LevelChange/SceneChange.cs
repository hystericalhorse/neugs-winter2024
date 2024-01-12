using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour, Interactable
{
    [SerializeField] string sceneName;

    public void OnInteract()
    {
        SceneManager.instance.LoadScene(sceneName);
    }

}
