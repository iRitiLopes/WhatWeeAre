using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPowerUp : MonoBehaviour {

    public PowerUpUI powerUpUI;

    [SerializeField]
    public List<PowerUpEffect> powerUpEffects = new();

    private void Start() {
        SceneManager.sceneLoaded += Reload;
    }

    // Start is called before the first frame update
    public void Reload(Scene scene, LoadSceneMode loadSceneMode) {
        StartCoroutine(Reload());
    }

    IEnumerator Reload() {
        yield return new WaitUntil(() => PowerUpUI.isReady);
        powerUpUI = FindObjectOfType<PowerUpUI>();
        powerUpEffects.ForEach(p => {
            powerUpUI.AddPowerUp(p);
            p.Apply(FindObjectOfType<Player>().gameObject, this);
        });
    }

    // Update is called once per frame
    void Update() {

    }

    public void AddPowerUp(PowerUpEffect powerUpEffect) {
        if (!powerUpEffects.Contains(powerUpEffect))
            powerUpEffects.Add(powerUpEffect);
        powerUpUI.AddPowerUp(powerUpEffect);
    }

    internal void AddPowerUp(PowerUpEffect powerUpEffect, float effectSeconds) {
        StartCoroutine(AddAndRemove(powerUpEffect, effectSeconds));
    }

    IEnumerator AddAndRemove(PowerUpEffect powerUpEffect, float seconds) {
        powerUpEffects.Add(powerUpEffect);
        powerUpUI.AddPowerUp(powerUpEffect);
        yield return new WaitForSeconds(seconds);
        powerUpEffects.Remove(powerUpEffect);
        powerUpUI.RemovePowerUp(powerUpEffect);
    }
}
