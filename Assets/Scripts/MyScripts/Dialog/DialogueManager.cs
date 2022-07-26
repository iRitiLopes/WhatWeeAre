using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
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

    public static bool isInitialized = false;
    private Queue<string> sentences;

    private string actualSentence;
    private Coroutine actualRoutine;
    
    // Start is called before the first frame update
    void Start() {
        sentences = new();
        nameTextTMP = nameText.GetComponent<TextMeshProUGUI>();
        dialogueTextTMP = dialogueText.GetComponent<TextMeshProUGUI>();
        isInitialized = true;
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

        actualSentence = sentences.Dequeue();
        actualRoutine = StartCoroutine(TypeSentence());
    }

    IEnumerator TypeSentence() {
        GameEvents.current.OnActionPressed -= DisplayNextSentence;
        GameEvents.current.OnActionPressed += CompleteSentence;
        dialogueTextTMP.text = "";
        foreach (var letter in actualSentence.ToCharArray()) {
            dialogueTextTMP.text += letter;
            yield return new WaitForSecondsRealtime(textSpeed);
        }
        yield return new WaitForSecondsRealtime(3.0f);
        GameEvents.current.OnActionPressed -= CompleteSentence;
        GameEvents.current.OnActionPressed += DisplayNextSentence;
        DisplayNextSentence();
    }

    private void CompleteSentence(){
        dialogueTextTMP.text = actualSentence;
        StopCoroutine(actualRoutine);
        GameEvents.current.OnActionPressed -= CompleteSentence;
        GameEvents.current.OnActionPressed += DisplayNextSentence;
    }

    private void EndDialogue() {
        actualSentence = "";
        animator.SetBool("isOpen", false);
        FindObjectOfType<GameManager>().Unpause();

        GameEvents.current.OnActionPressed -= CompleteSentence;
        GameEvents.current.OnActionPressed -= DisplayNextSentence;
        atFinish?.Invoke();
    }

    // Update is called once per frame
    void Update() {

    }
}
