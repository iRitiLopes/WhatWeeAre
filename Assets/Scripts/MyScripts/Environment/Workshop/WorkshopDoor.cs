using UnityEngine;

public class WorkshopDoor : MonoBehaviour {
    [SerializeField]
    bool isOnTheDoor = false;

    [SerializeField]
    readonly string scene;

    // Update is called once per frame
    void FixedUpdate() {
        if (this.isOnTheDoor && Input.GetKey(KeyCode.W)) {
            SceneHistory.LoadScene(scene);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player")) {
            return;
        }
        PlayerTooltip.show("Press W to enter");
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
