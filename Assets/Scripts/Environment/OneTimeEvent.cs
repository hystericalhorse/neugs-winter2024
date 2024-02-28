using UnityEngine;
using UnityEngine.Events;

public class OneTimeEvent : MonoBehaviour
{
    [SerializeField] UnityEvent evt;
    bool played;

	private void Start()
	{
		played = false;
	}

	public void Invoke()
	{
		if (!played)
		{
			evt?.Invoke();
			played= true;
		}
	}
}
