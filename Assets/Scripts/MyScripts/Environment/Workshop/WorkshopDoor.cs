using UnityEngine;

public class WorkshopDoor : MonoBehaviour
{

    [SerializeField]
    bool isOnTheDoor = false;

    [SerializeField]
    string scene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isOnTheDoor && Input.GetKey(KeyCode.W)) {
            SceneHistory.LoadScene(scene);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        PlayerTooltip.show("Press W to enter");
        this.isOnTheDoor = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        PlayerTooltip.hide();
        this.isOnTheDoor = false;
    }
}
