using System;
using System.Collections;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
    IEnumerator Start() {
        yield return new WaitUntil(() => GameEvents.current != null);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        GameEvents.current.OnEscPressed += PauseGame;
    }
    private void Awake() {
        
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