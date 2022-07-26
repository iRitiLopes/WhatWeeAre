using System;
using UnityEngine;

public class ComplexItemDisplay : MonoBehaviour {
    public GameObject[] dismantableItems;
    public Item item;

    public GameObject particle;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            PlayerTooltip.show("Press J to dismantle");
            GameEvents.current.OnActionPressed += Dismantle;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            PlayerTooltip.hide();
            GameEvents.current.OnActionPressed -= Dismantle;
        }
    }

    private void Dismantle() {
        foreach (var collectableItem in dismantableItems) {
            Instantiate(particle, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation);
            float x = transform.position.x + UnityEngine.Random.Range(-2.0f, 2.0f);
            float y = transform.position.y + UnityEngine.Random.Range(-2.0f, 2.0f);
            GameManager.createItem(position: new Vector3(x, y, 0),collectableItem: collectableItem);
            GameEvents.current.OnActionPressed -= Dismantle;
            Destroy(gameObject);
        }
    }
}