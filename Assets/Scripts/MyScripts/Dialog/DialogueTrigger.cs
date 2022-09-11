using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {
    private DialogueManager dialogueManager;
    public Dialogue dialogue;

    public bool alreadyDone = false;

    private void Start() {
        alreadyDone = FindObjectOfType<GameManager>().AlreadyDialogue(dialogue.dialogueName);
    }

    public void TriggerDialogue() {
        if (alreadyDone) {
            return;
        }
        dialogue.localize();
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, Finish);
    }

    void Finish() {
        dialogue.isDone = true;
        FindObjectOfType<GameManager>().FinishDialogue(dialogue.dialogueName);
    }
}
