using System.Collections;
using UnityEngine;

public class LoadSceneOnAwake : MonoBehaviour
{
    [SerializeField] string sceneName;

    void Start()
    {
        StartCoroutine(LateStart());
    }

    public IEnumerator LateStart()
    {
        yield return new WaitForFixedUpdate();
		SceneManager.instance.LoadScene(sceneName);
	}
}
