using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.IO;
using UnityEngine.UI;
using System;
using UnityEditor;
namespace Inventory {

    public class Inventory : MonoBehaviour {
        List<Tuple<Item, int>> items = new List<Tuple<Item, int>>();

        public GameObject slot;

        public String path;

        [SerializeField]
        private Canvas canvas;

        private void Start() {
            buildInventory();
            loadItems();
        }

        public void buildInventory() {

            JArray data = JArray.Parse(File.ReadAllText("./Assets/Sprites/items/player_items.json"));
            foreach (JObject item in data) {
                Guid id = Guid.Parse((string?)item.GetValue("id"));
                int quantity = ((int)item.GetValue("quantity"));
                Item it = ItemDatabase.findItem(id);

                if (it != null)
                    items.Add(new Tuple<Item, int>(it, quantity));
            }
        }

        public void loadItems() {
            int i = 0;
            foreach (var item in this.items) {
                var wrapper = new GameObject("A");
                wrapper.transform.parent = transform;
                wrapper.AddComponent<RectTransform>();
                wrapper.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                wrapper.AddComponent<CanvasGroup>();
                wrapper.AddComponent<DragDrop>();
                wrapper.GetComponent<DragDrop>().Canvas = canvas;
            
                var children = PrefabUtility.InstantiatePrefab(slot, wrapper.transform) as GameObject;
                //var children = (GameObject)Instantiate(slot.gameObject as GameObject, transform, false);
                wrapper.name = "InfinityItemSlot_" + i++;
                children.transform.Find("ItemSlot").GetComponent<Image>().sprite = item.Item1.icon;
                children.GetComponent<ItemSlot>().item = item.Item1;

                var qt = children.transform.Find("Quantity");
                qt.GetComponent<TMPro.TextMeshProUGUI>().text = item.Item2.ToString();
            }

        }

        private void FixedUpdate() {
            var rectTransform = GetComponent<RectTransform>();
            if(rectTransform.rect.height < 100) {
                var sizeFitter = GetComponent<ContentSizeFitter>();
                sizeFitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
                sizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
                rectTransform.sizeDelta = new Vector2(0, 200);
            }
        }

    }

}
