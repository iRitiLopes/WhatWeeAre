using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEditor;

public class WorkshopDoor : MonoBehaviour
{

    [SerializeField]
    bool isOnTheDoor = false;

    [SerializeField]
    SceneAsset scene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isOnTheDoor && Input.GetKey(KeyCode.W)) {
            SceneHistory.LoadScene(scene.name);
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
