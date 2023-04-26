using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointDialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueTrigger dialogueTrigger;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entrou no checkpoint");
            dialogueTrigger.TriggerDialogue();
        }
    }


}
