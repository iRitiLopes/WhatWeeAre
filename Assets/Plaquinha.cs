using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class Plaquinha : MonoBehaviour {

    private TextMeshProUGUI text;
    public string plaquinha_text;
    
    // Start is called before the first frame update
    void Start() {
        text = GetComponent<TextMeshProUGUI>();
        text.text = LocalizationSettings.StringDatabase.GetLocalizedString(tableReference: "tooltip", plaquinha_text);
    }

    // Update is called once per frame
    void Update() {
        text.text = LocalizationSettings.StringDatabase.GetLocalizedString(tableReference: "tooltip", plaquinha_text);
    }
}
