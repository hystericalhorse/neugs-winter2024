using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class PlayerCollisionHandler : MonoBehaviour
{
	[SerializeField] UnityEvent onCollisionStart;
	[SerializeField] UnityEvent onCollisionStay;
	[SerializeField] UnityEvent onCollisionExit;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.transform.GetComponent<PlayerController>())
			onCollisionStart?.Invoke();
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.transform.GetComponent<PlayerController>())
			onCollisionStay?.Invoke();
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.transform.GetComponent<PlayerController>())
			onCollisionExit?.Invoke();
	}
}
