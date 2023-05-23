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

    [SerializeField]
    Button quit;

    [SerializeField]
    Button back;

    [SerializeField]
    TextMeshProUGUI sceneName;

    [SerializeField]
    TextMeshProUGUI sceneDescription;
    // Start is called before the first frame update
    void Start() {
        refresh();
    }

    // Update is called once per frame
    void Update() {
        refresh();
    }


    public void refresh(){
        sceneName.text = LocalizationSettings.StringDatabase.GetLocalizedString("messages", "level_selection");
        sceneDescription.text = LocalizationSettings.StringDatabase.GetLocalizedString("messages", "level_selection_description");
        if(GameManager.gameManagerInstance.endGame){
            quit.gameObject.SetActive(true);
            back.interactable = false;
        } else {
            quit.gameObject.SetActive(false);
            back.interactable = true;
        }
        level1Button.interactable = true;
        level2Button.interactable = GameManager.isLevelReached("Level2Scene");
        level3Button.interactable = GameManager.isLevelReached("Level3Scene");
        level4Button.interactable = GameManager.isLevelReached("Level4Scene");

        level1Button.GetComponentInChildren<TextMeshProUGUI>().text = LocalizationSettings.StringDatabase.GetLocalizedString("levelsNames", "level1");
        level2Button.GetComponentInChildren<TextMeshProUGUI>().text = LocalizationSettings.StringDatabase.GetLocalizedString("levelsNames", "level2");
        level3Button.GetComponentInChildren<TextMeshProUGUI>().text = LocalizationSettings.StringDatabase.GetLocalizedString("levelsNames", "level3");
        level4Button.GetComponentInChildren<TextMeshProUGUI>().text = LocalizationSettings.StringDatabase.GetLocalizedString("levelsNames", "level4");
        quit.GetComponentInChildren<TextMeshProUGUI>().text = LocalizationSettings.StringDatabase.GetLocalizedString("messages", "quit_button");
    }
}
