using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedBox : MonoBehaviour, Interactable
{
    // Start is called before the first frame update
    //private short[] combination = new short[3];
    private bool locked = true;
    [SerializeField] private GameObject padLockPrefab;
    [SerializeField] private RotaryPadLocks padLock;

    public void OnInteract()
    {
        locked = padLock.locked;
        if (locked)
        {
            padLock.enabled = true;
            padLock.Activate();
        }
        else
        {
            Debug.Log("Yippeee!!!");
        }
    }

    private void Awake()
    {
        var temp = Instantiate(padLockPrefab.gameObject, FindAnyObjectByType<Canvas>().transform);
        padLock = temp.GetComponent<RotaryPadLocks>();
        padLock.GenerateRandomCombo();
    }
    public Vector3 GetComboVec3()
    {
        return padLock.GetComboVec3();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
