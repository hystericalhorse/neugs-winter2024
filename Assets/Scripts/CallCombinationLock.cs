using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CallCombinationLock : MonoBehaviour, Interactable
{
    // Start is called before the first frame update
    //private short[] combination = new short[3];
    [SerializeField] private GameObject combinationLock;
    //[SerializeField] private RotaryPadLocks padlock;
    [SerializeField] private GenerateCodeScript comboGen;
    [SerializeField] private TriggerDoor lockedDoor;
    //[SerializeField] private RotaryPadLocks padlock;
    [SerializeField] UnityEvent onUnlock;

    public void OnInteract()
    {
		combinationLock.SetActive(true);
		var comboLock = combinationLock.GetComponent<PadLockedTempController>();
        comboLock.Activate();
	
	}

    private void Awake()
    {
        var go = Instantiate(combinationLock.gameObject, FindAnyObjectByType<Canvas>().transform);

        combinationLock = go;

        var combination = combinationLock.GetComponent<PadLockedTempController>();
        if (combination != null) combination.onUnlock = onUnlock;
		combinationLock.SetActive(false);
	}

	[ContextMenu("Unlock")]
	public void Unlock()
    {
		lockedDoor.Unlock();
	}

    

 //   public Vector3 GetComboVec3()
   // {
    //    Vector3 output = new Vector3(combo[0], combo[1], combo[2]);
     //   return output;
   // }
}
