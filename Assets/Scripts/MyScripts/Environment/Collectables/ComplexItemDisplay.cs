using System;
using System.Collections;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class ComplexItemDisplay : MonoBehaviour {
    public GameObject[] dismantableItems;

    public ComplexItem complexItem;
    public GameObject particle;

    public Collider2D other;

    public string message = "Press J to dismantle";
    public string itemName = "";

    private IEnumerator Start() {
        var message = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("tooltip", "dismantle_item");
        message.Completed += (op) => {
            this.message = op.Result;
        };

        var itemName = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("complex_itens", complexItem.itemName);
        itemName.Completed += (op) => {
            this.itemName = itemName.Result;
        };
        yield return new WaitUntil(() => GameEvents.current != null);
        GameEvents.current.OnActionPressed += Dismantle;
        if (FindObjectOfType<GameManager>().AlreadyCollected(this)) {
            Destroy(gameObject);
        }
    }

    private void OnDestroy() {
        GameEvents.current.OnActionPressed -= Dismantle;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            this.other = other;
            Debug.Log("Ta perto");
            PlayerTooltip.show(this.message + " " + this.itemName);
            GameEvents.current.OnActionPressed += this.Dismantle;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            Debug.Log("Ta longe");
            this.other = null;
            GameEvents.current.OnActionPressed -= this.Dismantle;
            PlayerTooltip.hide();
        }
    }

    private void Dismantle() {
        if (other == null || !other.CompareTag("Player")) {
            return;
        }
        Debug.Log("Apertou");
        if (complexItem.dismantableItems.Length < 1) {
            DestroyItem();
            return;
        }
        Instantiate(particle, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation);
        foreach (var collectableItem in complexItem.dismantableItems) {
            DropItem(collectableItem);
        }
        FindObjectOfType<GameManager>().CollectItem(this);
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

    public string hash() {
        return gameObject.name + "_" + transform.position.x.ToString() + "_" + transform.position.y.ToString();
    }
}