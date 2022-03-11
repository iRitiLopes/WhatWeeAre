using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorkshopDoor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        PlayerTooltip.show("Press UP to enter");
    }

    private void OnTriggerExit2D(Collider2D other) {
        PlayerTooltip.hide();
    }
}
