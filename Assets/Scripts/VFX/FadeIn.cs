using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FadeIn : MonoBehaviour {
    [SerializeField] private bool Object = false;
    [SerializeField, Range(0, 0.25f)] private float ObjectFadeSpeed = 0.05f;

    [Header("Light")]
    [SerializeField] private bool Light = false;
    [SerializeField, Range(0, 0.25f)] private float LightFadeSpeed = 0.05f;

    [Header("Shader")]
    [SerializeField] private bool Shader = false;
    [SerializeField, Range(0, 0.25f)] private float ShaderFadeSpeed = 0.075f;

    private SpriteRenderer Sprite;
    private Light2D CurrentLight;
    private Material ShaderMaterial;

    void Start() {
        Sprite = GetComponent<SpriteRenderer>();

        if (Light) {
            CurrentLight = GetComponent<Light2D>();
            StartCoroutine(FadeInLight());
        }

        if (Shader) {
            ShaderMaterial = GetComponent<SpriteRenderer>().material;
            StartCoroutine(FadeInMaterial());
        }
        if (Object) StartCoroutine(FadeInObject());
    }

    IEnumerator FadeInObject() {
        for (float Transparency = 0; Transparency <= 1; Transparency += ObjectFadeSpeed) {
            Color color = Sprite.material.color;
            color.a = Transparency;
            Sprite.material.color = color;
            yield return new WaitForSeconds(.05f);
        }
    }

    IEnumerator FadeInLight() {
        for (float Transparency = 0; Transparency <= 1; Transparency += LightFadeSpeed) {
            Color color = CurrentLight.color;
            color.a = Transparency;
            CurrentLight.color = color;
            yield return new WaitForSeconds(.05f);
        }
    }

    IEnumerator FadeInMaterial() {
        for (float Fade = 0; Fade <= 1; Fade += ShaderFadeSpeed) {
            ShaderMaterial.SetFloat("_Fade", Fade);
            yield return new WaitForSeconds(.05f);
        }
    }
}