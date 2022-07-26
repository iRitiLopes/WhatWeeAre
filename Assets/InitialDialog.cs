using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialDialog : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        
        yield return new WaitUntil(() => DialogueManager.isInitialized);
        gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
