using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
    public GameObject nameText;
    public GameObject dialogueText;

    public Animator animator;

    private TextMeshProUGUI nameTextTMP;
    private TextMeshProUGUI dialogueTextTMP;

    [SerializeField]
    [Range(0.0f, 0.5f)]
    float textSpeed = 0.1f;

    Action atFinish;

    public bool isDone = false;
    private Queue<string> sentences;
    // Start is called before the first frame update
    void Start() {
        sentences = new();
        nameTextTMP = nameText.GetComponent<TextMeshProUGUI>();
        dialogueTextTMP = dialogueText.GetComponent<TextMeshProUGUI>();
    }

    public void StartDialogue(Dialogue dialogue, Action action) {
        FindObjectOfType<GameManager>().Pause();

        animator.SetBool("isOpen", true);
        nameTextTMP.text = dialogue.name;
        atFinish = action;

        sentences.Clear();
        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    private void DisplayNextSentence() {
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence) {
        dialogueTextTMP.text = "";
        foreach (var letter in sentence.ToCharArray()) {
            dialogueTextTMP.text += letter;
            yield return new WaitForSecondsRealtime(textSpeed);
        }
        yield return new WaitForSecondsRealtime(3.0f);
        DisplayNextSentence();
    }

    private void EndDialogue() {
        Debug.Log("End of conversation");
        animator.SetBool("isOpen", false);
        FindObjectOfType<GameManager>().Unpause();
        atFinish?.Invoke();
    }

    // Update is called once per frame
    void Update() {

    }
}
