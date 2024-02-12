using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningManager : MonoBehaviour {
    [SerializeField] private List<GameObject> Lightnings;
    [SerializeField] private int TimerMin = 5;
    [SerializeField] private int TimerMax = 10;
    private int RandomTimer;

    void Start() {
        RandomTimer = Random.Range(TimerMin, TimerMax);
        StartCoroutine(Countdown());
    }

    private void Thunder() {
        AudioManager.instance.PlaySound("Thunder");
    }

    private IEnumerator Countdown() {
        foreach (GameObject Lightning in Lightnings) {
            RandomTimer = Random.Range(TimerMin, TimerMax);
            Debug.Log("Random timer is " + RandomTimer);
            if (!Lightning.activeSelf) {
                yield return new WaitForSeconds(RandomTimer);
                Lightning.SetActive(true);
                yield return new WaitForSeconds(RandomTimer);
            }
        }
    }
}