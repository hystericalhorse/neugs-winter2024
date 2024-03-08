using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] Animator targetAnimator;
    [SerializeField] string targetProperty;

    public void SetTargetProperty(string value) => targetProperty = value;

    public void SetBool(bool value) => targetAnimator.SetBool(targetProperty, value);
    public void SetFloat(float value) => targetAnimator.SetFloat(targetProperty, value);
    public void Trigger() => targetAnimator.SetTrigger(targetProperty);
}
