using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;
using UnityEngine.UI;

public class PowerUpUI : MonoBehaviour {

    public PowerUpUISlot powerUpUI_1;
    public PowerUpUISlot powerUpUI_2;
    public PowerUpUISlot powerUpUI_3;
    public PowerUpUISlot powerUpUI_4;

    public static bool isReady = false;


    // Start is called before the first frame update
    void Start() {
        isReady = true;
        Debug.Log("start UI powerup");
        FindObjectOfType<PlayerPowerUp>().powerUpEffects.ForEach(x=> {
            Debug.Log(x);
            Debug.Log(transform.childCount);
            AddPowerUp(x);
        });
    }

    // Update is called once per frame
    void Update() {

    }

    public void AddPowerUp(PowerUpEffect powerUpEffect) {
        this.powerUpUI_1 = gameObject.transform.Find("PowerUp1").GetComponent<PowerUpUISlot>();
        this.powerUpUI_2 = gameObject.transform.Find("PowerUp2").GetComponent<PowerUpUISlot>();
        this.powerUpUI_3 = gameObject.transform.Find("PowerUp3").GetComponent<PowerUpUISlot>();
        this.powerUpUI_4 = gameObject.transform.Find("PowerUp4").GetComponent<PowerUpUISlot>();
        PowerUpUISlot emptyPowerUpUI = null;

        if (powerUpUI_1.currentPowerUp.Equals(powerUpEffect.powerUpName)) {
            return;
        } else if (powerUpUI_2.currentPowerUp.Equals(powerUpEffect.powerUpName)) {
            return;
        } else if (powerUpUI_3.currentPowerUp.Equals(powerUpEffect.powerUpName)) {
            return;
        } else if (powerUpUI_4.currentPowerUp.Equals(powerUpEffect.powerUpName)) {
            return;
        }

        if (!powerUpUI_1.isFull) {
            emptyPowerUpUI = powerUpUI_1;
        } else if (!powerUpUI_2.isFull) {
            emptyPowerUpUI = powerUpUI_2;
        } else if (!powerUpUI_3.isFull) {
            emptyPowerUpUI = powerUpUI_3;
        } else if (!powerUpUI_4.isFull) {
            emptyPowerUpUI = powerUpUI_4;
        }

        if (emptyPowerUpUI == null) {
            return;
        }
        emptyPowerUpUI.AddPowerUp(powerUpEffect);
    }

    public void RemovePowerUp(PowerUpEffect powerUpEffect) {
        PowerUpUISlot foundPowerUpUI = null;

        if (powerUpUI_1.isFull && powerUpUI_1.currentPowerUp.Equals(powerUpEffect.powerUpName)) {
            foundPowerUpUI = powerUpUI_1;
        } else if (powerUpUI_2.isFull && powerUpUI_2.currentPowerUp.Equals(powerUpEffect.powerUpName)) {
            foundPowerUpUI = powerUpUI_2;
        } else if (powerUpUI_3.isFull && powerUpUI_3.currentPowerUp.Equals(powerUpEffect.powerUpName)) {
            foundPowerUpUI = powerUpUI_3;
        } else if (powerUpUI_4.isFull && powerUpUI_4.currentPowerUp.Equals(powerUpEffect.powerUpName)) {
            foundPowerUpUI = powerUpUI_4;
        }

        if (!foundPowerUpUI) {
            return;
        }

        foundPowerUpUI.RemovePowerUp();
    }
}
