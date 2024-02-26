using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FadeOut : MonoBehaviour {
    [SerializeField] private bool Object = false;
    [SerializeField, Range(0, 0.25f)] private float ObjectFadeSpeed = 0.025f;

    [Header("Light")]
    [SerializeField] private bool Light = false;
    [SerializeField, Range(0, 0.25f)] private float LightFadeSpeed = 0.025f;

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
            StartCoroutine(FadeOutLight());
        }

        if (Shader) {
            ShaderMaterial = GetComponent<SpriteRenderer>().material;
            StartCoroutine(FadeOutMaterial());
        }
        if (Object) StartCoroutine(FadeOutObject());
    }

    IEnumerator FadeOutObject() {
        for (float Transparency = 1; Transparency >= 0; Transparency -= ObjectFadeSpeed) {
            Color color = Sprite.material.color;
            color.a = Transparency;
            Sprite.material.color = color;
            yield return new WaitForSeconds(.05f);
        }
    }

    IEnumerator FadeOutLight() {
        for (float Transparency = 1; Transparency >= 0; Transparency -= LightFadeSpeed) {
            Color color = CurrentLight.color;
            color.a = Transparency;
            CurrentLight.color = color;
            yield return new WaitForSeconds(.05f);
        }
    }

    IEnumerator FadeOutMaterial() {
        for (float Fade = 1; Fade >= 0; Fade -= ShaderFadeSpeed) {
            ShaderMaterial.SetFloat("_Fade", Fade);
            yield return new WaitForSeconds(.05f);
        }
    }
}