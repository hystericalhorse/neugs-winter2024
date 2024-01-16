using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour {
    [SerializeField] private float Battery = 100f;
    [SerializeField] private float DrainMultiplier = 1f;
    [SerializeField] private bool Active = true;

    void Start() {
        
    }

    void Update() {
        if (Battery <= 0) {
            this.gameObject.SetActive(false);
        }

        if (Active) {
            Battery -= Time.deltaTime * DrainMultiplier;
        }

        Debug.Log(Battery);
    }
}