using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueUI : MonoBehaviour {

    public static DialogueUI current;

    // Start is called before the first frame update
    void Start() {
        current = this;
    }

    private void Awake() {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {

    }

    public void Enable() {
        gameObject.SetActive(true);
    }

    public void Disable() {
        gameObject.SetActive(false);
    }
}
