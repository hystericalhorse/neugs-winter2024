using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private float Battery = 100f;
    [SerializeField] private float DrainMultiplier = 1f;
    [SerializeField] private bool Active = true;
    [SerializeField] private new Light2D light = null;
    [SerializeField] private AudioSource flashlight;

	private void Start()
	{
		light = gameObject.GetComponent<Light2D>();
        
	}

	void Update()
    {
        if (!Active) return;

        if (Battery <= 0)
        {
			Active = false;
            light.enabled = false;
            flashlight.Stop();
		}

		if (Active) Battery -= Time.deltaTime * DrainMultiplier;
    }

    public void Toggle()
    {
        light.enabled = !light.enabled;
        flashlight.Play();
        Active = light.enabled;
    }
}