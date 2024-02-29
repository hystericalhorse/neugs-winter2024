using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class AreaTrigger : MonoBehaviour
{
    [SerializeField] new Collider2D collider;
    public UnityEvent OnEnter;
    public UnityEvent OnStay;
    public UnityEvent OnExit;

    public void Start()
    {
        collider ??= GetComponent<Collider2D>();
        collider.isTrigger = true;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    { if (collision.GetComponent<PlayerController>()) OnEnter?.Invoke(); }
	public void OnTriggerStay2D(Collider2D collision) 
    { if (collision.GetComponent<PlayerController>()) OnStay?.Invoke();}
	public void OnTriggerExit2D(Collider2D collision) 
    { if (collision.GetComponent<PlayerController>()) OnExit?.Invoke();}
}
