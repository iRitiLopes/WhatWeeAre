using System;
using UnityEngine;

public class ComplexItemDisplay : MonoBehaviour {
    public GameObject[] dismantableItems;
    public Item item;

    public ComplexItem complexItem;
    public GameObject particle;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            Debug.Log("Ta perto");
            PlayerTooltip.show("Press J to dismantle " + complexItem.itemName);
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
        Debug.Log("Apertou");
        if (complexItem.dismantableItems.Length < 1) {
            DestroyItem();
            return;
        }
        Instantiate(particle, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation);
        foreach (var collectableItem in complexItem.dismantableItems) {
            DropItem(collectableItem);
        }
        DestroyItem();
    }

    private void DropItem(GameObject item) {
        float x = transform.position.x + UnityEngine.Random.Range(-2.0f, 2.0f);
        float y = transform.position.y + UnityEngine.Random.Range(-2.0f, 2.0f);
        GameManager.createItem(position: new Vector3(x, y, 0), collectableItem: item);
    }

    private void DestroyItem() {
        Debug.Log("Desmontou");
        GameEvents.current.OnActionPressed -= Dismantle;
        Destroy(gameObject);
    }
}