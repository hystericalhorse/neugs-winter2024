using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

public class TransitionScreen : MonoBehaviour
{
    [SerializeField] Image image;
	[SerializeField] CanvasGroup canvasGroup;
	public float transitionTime = 0f;
	public UnityEvent onTransitionBegin;
	public UnityEvent onTransitionEnd;

	private void Awake()
	{
		image = GetComponent<Image>() ?? gameObject.AddComponent<Image>();
		canvasGroup = GetComponent<CanvasGroup>() ?? gameObject.AddComponent<CanvasGroup>();

		canvasGroup.alpha = 0;
	}

	public IEnumerator FadeIn()
	{
		StopCoroutine(FadeOut());

		if (transitionTime <= 0)
		{
			canvasGroup.alpha = 1;
			StopCoroutine(FadeIn());
		}

		while (canvasGroup.alpha < 1)
		{
			canvasGroup.alpha += Time.deltaTime * transitionTime;
			yield return null;
		}

		//for (float f = transitionTime; f > 0; f -= Time.deltaTime)
		//{
		//	canvasGroup.alpha += Time.deltaTime * transitionTime;
		//	yield return null;
		//}

		canvasGroup.alpha = 1;
		onTransitionBegin?.Invoke();
		onTransitionBegin?.RemoveAllListeners();
	}

	public IEnumerator FadeOut()
	{
		StopCoroutine(FadeIn());

		if (transitionTime <= 0)
		{
			canvasGroup.alpha = 0;
			StopCoroutine(FadeOut());
		}

		while (canvasGroup.alpha > 0)
		{
			canvasGroup.alpha -= Time.deltaTime * transitionTime;
			yield return null;
		}

		//for (float f = transitionTime; f > 0; f -= Time.deltaTime)
		//{
		//	canvasGroup.alpha -= Time.deltaTime * transitionTime;
		//	yield return null;
		//}

		canvasGroup.alpha = 0;

		onTransitionEnd?.Invoke();
		onTransitionEnd?.RemoveAllListeners();
	}
}
