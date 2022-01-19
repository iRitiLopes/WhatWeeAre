using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Inventory {

    public class Inventory : MonoBehaviour {
        List<Item> items;
        public ItemDatabase database;

        public GameObject itemPrefab;
        private void Start() {
            database = gameObject.AddComponent<ItemDatabase>();
            var size = database.size();
            var child = Instantiate(itemPrefab, this.transform, false);
            child.transform.position = transform.position;
        }
        void put() {
            var child = Instantiate(itemPrefab, this.transform, false);
            child.transform.localPosition = new Vector3(0, 0, 0);
        }
    }



}
