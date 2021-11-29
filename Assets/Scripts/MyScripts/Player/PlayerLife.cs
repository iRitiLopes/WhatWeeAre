using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour {
    public Text text;
    // Start is called before the first frame update
    void Start() {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    private void FixedUpdate() {
        var player = FindObjectOfType<Player>();
        text.text = $"{player.lifes}x";
    }
}
