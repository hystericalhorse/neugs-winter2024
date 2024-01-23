using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private float Battery = 100f;
    [SerializeField] private float DrainMultiplier = 1f;
    [SerializeField] private bool Active = true;
    [SerializeField] private new Light2D light = null;
    [SerializeField] private PolygonCollider2D Hitbox = null;

	private void Start()
	{
		light = gameObject.GetComponent<Light2D>();
        Hitbox = gameObject.GetComponent<PolygonCollider2D>();
	}

	void Update()
    {
        if (!Active) return;

        if (Battery <= 0)
        {
			Active = false;
            light.enabled = false;
            AudioManager.instance.StopSounds();
		}

		if (Active) Battery -= Time.deltaTime * DrainMultiplier;
    }

    public void Toggle()
    {
        Hitbox.enabled = !Hitbox.enabled;
        light.enabled = !light.enabled;
        AudioManager.instance.PlaySound("Flashlight");
        Active = light.enabled;
        Active = Hitbox.enabled;
    }
}