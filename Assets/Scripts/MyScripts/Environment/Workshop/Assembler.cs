using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class Assembler : MonoBehaviour, Notificable {
    [SerializeField]
    GameObject outputSlot;

    [SerializeField]
    GameObject input1;

    [SerializeField]
    GameObject input2;

    [SerializeField]
    GameObject input3;

    [SerializeField]
    Canvas canvas;

    [SerializeField]
    GameObject slot;

    [SerializeField]
    Message messageManager;

    private static Assembler instance;

    ItemSlot actualItem = null;

    PowerUpEffect actualPowerUpEffect = null;

    List<PowerUpEffect> powerUpEffects = new();

    Dictionary<string, ItemSlot> inputItens = new();

    [SerializeField] private RadialIndicator loading;

    private void Awake() {
        instance = this;
        input1.GetComponent<Dropable>().subscribe(this);
        input2.GetComponent<Dropable>().subscribe(this);
        input3.GetComponent<Dropable>().subscribe(this);
        powerUpEffects = FindObjectOfType<RecipesDisplay>().powerUpEffects;
    }

    private void FixedUpdate() {
        notify(null);
    }

    public void notify(GameObject go) {
        var i1 = input1.transform.childCount > 0 ? input1.transform.GetChild(0).GetChild(0)?.GetComponent<ItemSlot>() : null;
        var n1 = input1.name;

        var i2 = input2.transform.childCount > 0 ? input2.transform.GetChild(0).GetChild(0)?.GetComponent<ItemSlot>() : null;
        var n2 = input2.name;

        var i3 = input3.transform.childCount > 0 ? input3.transform.GetChild(0).GetChild(0)?.GetComponent<ItemSlot>() : null;
        var n3 = input3.name;

        if(i1 == null && i2 == null && i3 == null){
            CleanOutput();
            CleanDisassemble();
            return;
        }

        AddInputItem(i1, n1);
        AddInputItem(i2, n2);
        AddInputItem(i3, n3);

        Debug.Log(inputItens.Keys.Select(x => x).Aggregate((x, y) => x + " " + y));

        var found = ItemDatabase.findItemByComponents(inputItens.Values.ToList());
        var powerUpFound = powerUpEffects.Where(p => p.canCreate(inputItens.Values.ToList())).ToList();
        Debug.Log(found);
        if (found != null) {
            AddOutputItem(found);
        } else if (powerUpFound.Count() > 0) {
            Debug.Log(powerUpFound[0].name);
            AddPowerUpInput(powerUpFound[0]);
        } else {
            CleanOutput();
            CleanDisassemble();
        }
    }

    private void AddPowerUpInput(PowerUpEffect powerUpEffect) {
        if(actualPowerUpEffect != null && actualPowerUpEffect.name.Equals(powerUpEffect.name)){
            return;
        } else {
            CleanOutput();
        }
        GameObject wrapper;
        wrapper = CreateDragDropObject.createWrapper(outputSlot.transform, canvas, false);
        putItem(wrapper, powerUpEffect, 1);
        wrapper.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
        wrapper.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
        wrapper.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
    }

    private void AddInputItem(ItemSlot item, string name) {
        if (item == null) {
            inputItens.Remove(name);
            return;
        }
        inputItens.Remove(name);
        inputItens.Add(name, item);
    }

    private void AddOutputItem(Item item) {
        if(actualItem != null && actualItem.item.name.Equals(item.name)){
            return;
        }
        GameObject wrapper;
        wrapper = CreateDragDropObject.createWrapper(outputSlot.transform, canvas, false);
        putItem(wrapper, item, 1);
        wrapper.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
        wrapper.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
        wrapper.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
    }

    private void putItem(GameObject wrapper, Item item, int quantity) {
        var children = UnityEngine.Object.Instantiate(slot, wrapper.transform) as GameObject;
        wrapper.name = "InfinityItemSlotWrapper_" + outputSlot;
        children.transform.Find("ItemSlot").GetComponent<Image>().sprite = item.icon;
        children.GetComponent<ItemSlot>().SetItem(item, quantity);
        actualItem = children.GetComponent<ItemSlot>();
    }

    private void putItem(GameObject wrapper, PowerUpEffect powerUpEffect, int quantity) {
        var children = UnityEngine.Object.Instantiate(slot, wrapper.transform) as GameObject;
        wrapper.name = "InfinityItemSlotWrapper_" + outputSlot;
        children.transform.Find("ItemSlot").GetComponent<Image>().sprite = powerUpEffect.sprite;
        children.GetComponent<Slot>().name = powerUpEffect.name;
        children.GetComponent<Slot>().quantity = quantity;
        children.GetComponent<Slot>().description = powerUpEffect.description;
        actualPowerUpEffect = powerUpEffect;
    }

    public static void assemble() {
        instance.loading.load(instance.__assemble);
    }

    public void __assemble(){
        if (instance.actualItem != null) {
            instance._assemble();
            instance.CleanDisassemble();
            return;
        }

        if (instance.actualPowerUpEffect != null) {
            instance._assemblePowerUp();
            instance.CleanDisassemble();
            return;
        }
        instance.messageManager.ShowMessage("Assembler", LocalizationSettings.StringDatabase.GetLocalizedString("messages", "no_item"));
        Debug.Log("This item doesnt provide");
    }

    private void _assemblePowerUp() {
        string message = "";
        if (FindObjectOfType<PlayerPowerUp>().AlreadyHasPowerUp(actualPowerUpEffect)) {
            return;
        }

        FindObjectOfType<PlayerPowerUp>().AddPowerUpEffect(actualPowerUpEffect);
        foreach (var inputItem in inputItens.Values) {
            message += $" - {inputItem.item.name} x{actualPowerUpEffect.rawItems.ToList().Find(x => x.id.Equals(inputItem.item.id)).quantity}\n";
            Inventory.removeItem(inputItem.item.id, actualPowerUpEffect.rawItems.ToList().Find(x => x.id.Equals(inputItem.item.id)).quantity);
            inputItem.quantity = actualPowerUpEffect.rawItems.ToList().Find(x => x.id.Equals(inputItem.item.id)).quantity;
        }
        message += $"---------------------------- \n";
        message += $" - {actualPowerUpEffect.name}";
        messageManager.ShowMessage("Assembler", message);
    }

    private void _assemble() {
        string message = "";
        Inventory.AddItem(actualItem.item.id, 1);
        foreach (var inputItem in inputItens.Values) {
            message += $" - {inputItem.item.name} x{actualItem.item.RawItems.Find(x => x.id.Equals(inputItem.item.id)).quantity}\n";
            Inventory.removeItem(inputItem.item.id, actualItem.item.RawItems.Find(x => x.id.Equals(inputItem.item.id)).quantity);
            inputItem.quantity = actualItem.item.RawItems.Find(x => x.id.Equals(inputItem.item.id)).quantity;
        }
        message += $"---------------------------- \n";
        message += $" - {actualItem.item.name}";
        messageManager.ShowMessage("Assembler", message);
    }

    private void CleanDisassemble() {
        if (actualItem != null || actualPowerUpEffect != null) {
            CleanOutput();
            CleanInputItems();
        }
    }

    private void CleanOutput(){
        
            var itemSlot = ChildFinder.findWithStartName(outputSlot.transform, "InfinityItemSlotWrapper");
            inputItens = new();

            if(itemSlot != null)
                Destroy(itemSlot.gameObject);
            actualItem = null;
            actualPowerUpEffect = null;
    }

    private void CleanInputItems() {
        foreach (Transform child in input1.transform) {
            Destroy(child.gameObject);
        }

        foreach (Transform child in input2.transform) {
            Destroy(child.gameObject);
        }

        foreach (Transform child in input3.transform) {
            Destroy(child.gameObject);
        }
        inputItens = new();
    }

}
