using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class LockedBox : MonoBehaviour, Interactable
{
    // Start is called before the first frame update
    //private short[] combination = new short[3];
    [SerializeField] private GameObject padlockGO;
    //[SerializeField] private RotaryPadLocks padlock;
    [SerializeField] private List<int> combo;
    //[SerializeField] private RotaryPadLocks padlock;
    [SerializeField] UnityEvent onUnlock;

    public void OnInteract()
    {
		var rotary = padlockGO.GetComponent<RotaryPadLocks>();
		if (rotary != null)
		{
			padlockGO.SetActive(true);
			rotary.Activate();
		}
		else
		{
			var comboLock = padlockGO.GetComponent<PadLockedTempController>();
		}
	}

    private void Awake()
    {
        var go = Instantiate(padlockGO, FindAnyObjectByType<Canvas>().transform);

        padlockGO = go;
		padlockGO.SetActive(true);
	}

	private void Start()
	{
		var rotary = padlockGO.GetComponent<RotaryPadLocks>();
		if (rotary != null)
		{
			rotary.onUnlock += () =>
			{
				onUnlock?.Invoke();
			};
			
			combo = rotary.GetComboInt().ToList();
		}
	}

	[ContextMenu("Unlock")]
	public void Unlock()
    {
		var rotary = padlockGO.GetComponent<RotaryPadLocks>();
        rotary.onUnlock?.Invoke();
	}

    public Vector3 GetComboVec3()
    {
        Vector3 output = new Vector3(combo[0], combo[1], combo[2]);
        return output;
    }
}
