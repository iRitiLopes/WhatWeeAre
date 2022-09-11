using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class WorkshopDoor : MonoBehaviour {
    [SerializeField]
    bool isOnTheDoor = false;

    [SerializeField]
    string scene;


    [SerializeField]
    string message = "Press W to enter";

    // Update is called once per frame
    void Start() {
        var op = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("tooltip", "enter_door");
        op.Completed += (op) => {
            message = op.Result;
        };
    }
    void FixedUpdate() {
        if (this.isOnTheDoor && Input.GetKey(KeyCode.W) && !FindObjectOfType<GameManager>().isPaused) {
            SceneHistory.LoadScene(scene);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player")) {
            return;
        }
        PlayerTooltip.show(message);
        this.isOnTheDoor = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (!other.CompareTag("Player")) {
            return;
        }
        PlayerTooltip.hide();
        this.isOnTheDoor = false;
    }
}
