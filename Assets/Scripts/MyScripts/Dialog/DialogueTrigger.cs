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


    private string path = Application.streamingAssetsPath + "/";
    // Start is called before the first frame update

    private void Start() {
        path = path + dialogue.name.Replace(" ", "_") + dialogue.GetHash() + ".json";

        try{
            JObject data = JObject.Parse(File.ReadAllText(path));
            var alreadyDone = (bool) data.GetValue("isDone");
            this.alreadyDone = alreadyDone || dialogue.isDone;
        } catch (Exception e) {
            Debug.Log(e.Message);
        }
    }

    public void TriggerDialogue() {
        if(alreadyDone){
            return;
        }
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, Finish);
    }

    void Finish() {
        dialogue.isDone = true;
        Persist();
    }

    private void Persist() {
        var data = JsonUtility.ToJson(dialogue);
        File.WriteAllText(path, data);
    }
}
