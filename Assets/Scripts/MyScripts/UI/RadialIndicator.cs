using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialIndicator : MonoBehaviour {

    [Header("Radial Timers")]
    [SerializeField] private float indicatorTimer = 0.0f;
    [SerializeField] private float maxIndicatorTimer = 1.0f;

    [Header("UI Indicator")]
    [SerializeField] private Image radialIndicatorUI = null;

    [SerializeField] private float timeInSeconds = 3;

    [SerializeField] private AudioSource audioSource;

    private bool enable = false;


    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        if (enable)
            indicatorTimer += Time.deltaTime;
    }

    public void load(Action finalAction) {
        radialIndicatorUI.fillAmount = 0;
        indicatorTimer = 0;
        StartCoroutine(LoadAll(finalAction));

    }

    IEnumerator LoadAll(Action finalAction) {
        enable = true;
        GameManager.gameManagerInstance.Pause();
        while (indicatorTimer <= timeInSeconds) {
            radialIndicatorUI.fillAmount = indicatorTimer / timeInSeconds;
            yield return new WaitForEndOfFrame();
            audioSource.Play();
        }
        GameManager.gameManagerInstance.Unpause();
        enable = false;
        radialIndicatorUI.fillAmount = 0;
        finalAction();
    }

    public bool isCompleted(){
        return radialIndicatorUI.fillAmount >= 0.99;
    }
}
