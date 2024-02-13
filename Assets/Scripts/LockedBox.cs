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
		padlockGO.SetActive(true);
		var rotary = padlockGO.GetComponent<RotaryPadLocks>();
		if (rotary != null) { rotary.Activate(); }
		else
		{
			var comboLock = padlockGO.GetComponent<PadLockedTempController>();
		}
	}

    private void Awake()
    {
        var go = Instantiate(padlockGO.gameObject, FindAnyObjectByType<Canvas>().transform);

        padlockGO = go;

        var rotary = padlockGO.GetComponent<RotaryPadLocks>();
        if (rotary != null)
        {
            rotary.onUnlock += () =>
            {
                onUnlock?.Invoke();
            };

            combo = rotary.GetComboInt().ToList();

			padlockGO.SetActive(false);
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
