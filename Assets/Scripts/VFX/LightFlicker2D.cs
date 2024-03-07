using UnityEngine.Rendering.Universal;
using UnityEngine;

[RequireComponent(typeof(Light2D))]
public class LightFlicker2D : MonoBehaviour
{
	[SerializeField] float minIntensity = 1;
	[SerializeField] float maxIntensity = 3;

	[SerializeField, Range(0.1f, 1f)] float smoothAmount = 0.1f;

	Light2D lightComponent;

	private void Start()
	{
		lightComponent = GetComponent<Light2D>();
		if (maxIntensity < minIntensity) maxIntensity = minIntensity + 1;
		lightComponent.intensity = minIntensity;

		targetIntensity = Random.Range(minIntensity, maxIntensity);
	}

	float targetIntensity;
	private void Update()
    {
		float f = Mathf.Abs(lightComponent.intensity / (targetIntensity - lightComponent.intensity)) * (Time.smoothDeltaTime / smoothAmount);
		lightComponent.intensity = Mathf.SmoothStep(lightComponent.intensity, targetIntensity, f);
    }

	private void LateUpdate()
	{
		if (Mathf.Approximately(lightComponent.intensity, targetIntensity))
		{
			targetIntensity = Random.Range(minIntensity, maxIntensity);
		}
	}
}
