using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class ChangeLanguage : MonoBehaviour {

    public Langs lang;

    public bool loadScene = true;
    // Start is called before the first frame update
    void Start() {
   
    }

    // Update is called once per frame
    void Update() {

    }


    public void change() {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[((int)lang)];

        if(!loadScene){
            return;
        }

        FindObjectOfType<GameManager>().Pause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.gameManagerInstance.Unpause();
    }

    public void changeButtonColor(){
        GetComponent<UnityEngine.UI.Button>().colors = new UnityEngine.UI.ColorBlock() {
            normalColor = new Color(0.5f, 0.5f, 0.5f, 1),
            highlightedColor = new Color(0.5f, 0.5f, 0.5f, 1),
            pressedColor = new Color(0.5f, 0.5f, 0.5f, 1),
            selectedColor = new Color(0.5f, 0.5f, 0.5f, 1),
            disabledColor = new Color(0.5f, 0.5f, 0.5f, 1),
            colorMultiplier = 1,
        };
    }
}
