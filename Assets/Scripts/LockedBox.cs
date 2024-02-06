using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LockedBox : MonoBehaviour, Interactable
{
    // Start is called before the first frame update
    //private short[] combination = new short[3];
    private bool locked = true;
    [SerializeField] private GameObject padlockGO;
    [SerializeField] private RotaryPadLocks padlock;
    [SerializeField] UnityEvent onUnlock;


    public void OnInteract()
    {
        locked = padlock.locked;
     

        if (locked)
        {
            padlockGO.SetActive(true);
            padlock.Activate();
        }
        else
        {
            
        }
    }

    private void Awake()
    {
        var go = Instantiate(padlockGO.gameObject, FindAnyObjectByType<Canvas>().transform);
        padlock = go.GetComponent<RotaryPadLocks>();
        padlock.GenerateRandomCombo();

        padlock.onUnlock += () =>
        {
            onUnlock?.Invoke();
        };

        padlockGO = go;
        padlockGO.SetActive(false);
    }

    public Vector3 GetComboVec3()
    {
        return padlock.GetComboVec3();
    }
}
