using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPoint : MonoBehaviour {

    public string NextLevel = null;
    public float LoadDelay = 3f;

    private float counter = 0;
    private bool loadNextLevel = false;

    public bool isFinalLevel = false;

    public Dialogue finalDialogue;

    GameObject gameData;

    void Start() {
        finalDialogue.localize();
        gameData = GameObject.Find("GameData");
    }

    // Update is called once per frame
    void Update() {
        if (loadNextLevel == true) {
            counter += Time.deltaTime;
            if (counter >= LoadDelay) {
                if (gameData != null) {
                    gameData.GetComponent<DataControl>().Save();
                }

                loadNextLevel = false;
                GameManager.gameManagerInstance.firstRun = true;
                SceneHistory.LoadScene(NextLevel);
            }
        }
    }

    void Finish() {
        GameControl game = GameObject.Find("Main Camera").GetComponent<GameControl>();
        if (game.IsGameRuning) {
            game.IsGameRuning = false;
            if (string.IsNullOrEmpty(NextLevel) == false) {
                loadNextLevel = true;
            }
        }
        FindObjectOfType<GameManager>().FinishDialogue(finalDialogue.dialogueName);
        SceneHistory.LoadScene(NextLevel);
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.CompareTag("Player")) {
            if (isFinalLevel && finalDialogue != null) {
                FindObjectOfType<DialogueManager>().StartDialogue(finalDialogue, Finish);
            }

        }
    }

}
