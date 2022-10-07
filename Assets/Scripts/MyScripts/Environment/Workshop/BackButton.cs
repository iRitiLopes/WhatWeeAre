using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class BackButton : MonoBehaviour {

    public TextMeshProUGUI textMeshProUGUI;
    // Start is called before the first frame update
    void Start() {
        gameObject.GetComponent<Button>().onClick.AddListener(Back);
        textMeshProUGUI.text = LocalizationSettings.StringDatabase.GetLocalizedString("messages", "back_button");
    }

    // Update is called once per frame
    void Update() {

    }

    private void FixedUpdate() {

    }

    public void Back() {
        SceneHistory.PreviousScene();
    }
}
