using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour {

    [SerializeField]
    Button level1Button;
    [SerializeField]
    Button level2Button;
    [SerializeField]
    Button level3Button;
    [SerializeField]
    Button level4Button;
    // Start is called before the first frame update
    void Start() {
        level1Button.interactable = GameManager.isLevelReached("Level1Scene");
        level2Button.interactable = GameManager.isLevelReached("Level2Scene");
        level3Button.interactable = GameManager.isLevelReached("Level3Scene");
        level4Button.interactable = GameManager.isLevelReached("Level4Scene");
    }

    // Update is called once per frame
    void Update() {

    }
}
