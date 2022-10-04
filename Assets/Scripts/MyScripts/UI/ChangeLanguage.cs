using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class ChangeLanguage : MonoBehaviour {

    public Langs lang;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }


    public void change() {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[((int)lang)];
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
