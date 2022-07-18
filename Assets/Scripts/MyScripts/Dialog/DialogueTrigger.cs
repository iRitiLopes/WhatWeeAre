using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {
    private DialogueManager dialogueManager;
    public Dialogue dialogue;

    private string path = "./Assets/Persistance/Dialogues/";
    // Start is called before the first frame update

    private void Start() {
        path = path + dialogue.name.Replace(" ", "_") + ".json";

        JObject data = JObject.Parse(File.ReadAllText(path));
        var alreadyDone = (bool) data.GetValue("isDone");
        if (alreadyDone || dialogue.isDone) {
            return;
        }

        TriggerDialogue();
    }

    public void TriggerDialogue() {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, Finish);

    }

    void Finish() {
        dialogue.isDone = true;
        Persist();
    }

    private void Persist() {
        var data = JsonUtility.ToJson(dialogue);
        File.WriteAllText(path, data);
        Debug.Log(data);
    }
}
