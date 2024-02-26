using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
using Unity.VisualScripting;

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

	public void Transition(float seconds = 0)
	{
		StopCoroutine(FadeIn());
		StopCoroutine(FadeOut());

		StartCoroutine(FadeIn(true));
	}

	public IEnumerator FadeIn(bool fadeOut = false)
	{
		StopCoroutine(FadeOut());

		onTransitionBegin?.Invoke();
		onTransitionBegin?.RemoveAllListeners();

		if (transitionTime <= 0)
		{
			canvasGroup.alpha = 1;
			StopCoroutine(FadeIn());
		}

		float time = 0;
		while (canvasGroup.alpha < 1)
		{
			canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 1, time / transitionTime);
			time += Time.deltaTime;
			yield return null;
		}

		//for (float f = transitionTime; f > 0; f -= Time.deltaTime)
		//{
		//	canvasGroup.alpha += Time.deltaTime * transitionTime;
		//	yield return null;
		//}

		yield return new WaitForFixedUpdate();
		canvasGroup.alpha = 1;

		if (fadeOut) StartCoroutine(FadeOut());
	}

	public IEnumerator FadeOut()
	{
		StopCoroutine(FadeIn());

		onTransitionEnd?.Invoke();
		onTransitionEnd?.RemoveAllListeners();

		if (transitionTime <= 0)
		{
			canvasGroup.alpha = 0;
			StopCoroutine(FadeOut());
		}

		float time = 0;
		while (canvasGroup.alpha > 0)
		{
			canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 0, time / transitionTime);
			time += Time.deltaTime;
			yield return null;
		}

		//for (float f = transitionTime; f > 0; f -= Time.deltaTime)
		//{
		//	canvasGroup.alpha -= Time.deltaTime * transitionTime;
		//	yield return null;
		//}

		yield return new WaitForFixedUpdate();

		canvasGroup.alpha = 0;
	}
}
