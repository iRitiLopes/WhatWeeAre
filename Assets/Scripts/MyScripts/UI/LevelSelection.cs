using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour {

    [SerializeField]
    Button level1Button;
    [SerializeField]
    Button level2Button;
    [SerializeField]
    Button level3Button;
    [SerializeField]
    Button level4Button;
    // Start is called before the first frame update
    void Start() {
        level1Button.interactable = true;
        level2Button.interactable = GameManager.isLevelReached("Level2Scene");
        level3Button.interactable = GameManager.isLevelReached("Level3Scene");
        level4Button.interactable = GameManager.isLevelReached("Level4Scene");

        level1Button.GetComponentInChildren<TextMeshProUGUI>().text = LocalizationSettings.StringDatabase.GetLocalizedString("levelsNames", "level1");
        level2Button.GetComponentInChildren<TextMeshProUGUI>().text = LocalizationSettings.StringDatabase.GetLocalizedString("levelsNames", "level2");
        level3Button.GetComponentInChildren<TextMeshProUGUI>().text = LocalizationSettings.StringDatabase.GetLocalizedString("levelsNames", "level3");
        level4Button.GetComponentInChildren<TextMeshProUGUI>().text = LocalizationSettings.StringDatabase.GetLocalizedString("levelsNames", "level4");
    }

    // Update is called once per frame
    void Update() {

    }
}
