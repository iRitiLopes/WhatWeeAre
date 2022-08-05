using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpUISlot : MonoBehaviour {
    public string currentPowerUp = "";

    public bool isFull = false;
    void Start() {
    }

    // Update is called once per frame
    void Update() {

    }

    public void AddPowerUp(PowerUpEffect powerUpEffect) {
        gameObject.GetComponent<Image>().sprite = powerUpEffect.sprite;
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        gameObject.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = powerUpEffect.powerUpName;
        isFull = true;
        currentPowerUp = powerUpEffect.powerUpName;
    }

    public void RemovePowerUp(){
        gameObject.GetComponent<Image>().sprite = null;
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        gameObject.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = "";
        isFull = false;
        currentPowerUp = "";
    }
}
