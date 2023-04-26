using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class PauseMenu : MonoBehaviour {

    [SerializeField]
    TextMeshProUGUI quit;

    [SerializeField]
    TextMeshProUGUI levelSelect;

    [SerializeField]
    TextMeshProUGUI language;
    IEnumerator Start() {
        yield return new WaitUntil(() => GameEvents.current != null);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        GameEvents.current.OnEscPressed += PauseGame;

        quit.text = LocalizationSettings.StringDatabase.GetLocalizedString("messages", "quit_button");
        levelSelect.text = LocalizationSettings.StringDatabase.GetLocalizedString("messages", "level_button");
        language.text = LocalizationSettings.StringDatabase.GetLocalizedString("messages", "language_button");
    }
    private void Awake() {

    }

    public void pause(){
        PauseGame();
    }

    private void PauseGame() {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        FindObjectOfType<GameManager>().Pause();
        GameEvents.current.OnEscPressed -= PauseGame;
        GameEvents.current.OnEscPressed += UnpauseGame;
    }

    private void UnpauseGame() {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        FindObjectOfType<GameManager>().Unpause();
        GameEvents.current.OnEscPressed -= UnpauseGame;
        GameEvents.current.OnEscPressed += PauseGame;
    }
}