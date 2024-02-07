using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class LockedBox : MonoBehaviour, Interactable
{
    // Start is called before the first frame update
    //private short[] combination = new short[3];
    private bool locked = true;
    [SerializeField] private GameObject padlockGO;
    //[SerializeField] private RotaryPadLocks padlock;
    [SerializeField] private List<int> combo;
    //[SerializeField] private RotaryPadLocks padlock;
    [SerializeField] UnityEvent onUnlock;

    public void OnInteract()
    {
        //locked = padlockGO.locked;
     

        if (locked)
        {
            padlockGO.SetActive(true);
            var rotary = padlockGO.GetComponent<RotaryPadLocks>();
            if (rotary != null) { rotary.Activate(); }
            else
            {
                var comboLock = padlockGO.GetComponent<PadLockedTempController>();
            }
            //padlock.Activate();
        }
        else
        {
            
        }
    }

    private void Awake()
    {
        var go = Instantiate(padlockGO.gameObject, FindAnyObjectByType<Canvas>().transform);
        //padlock = go.GetComponent<RotaryPadLocks>();
        //padlock.GenerateRandomCombo();


        padlockGO = go;
        padlockGO.SetActive(false);

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

    public void Unlock()
    {
        locked = false;
    }

    public Vector3 GetComboVec3()
    {
        Vector3 output = new Vector3(combo[0], combo[1], combo[2]);
        return output;
    }
}
