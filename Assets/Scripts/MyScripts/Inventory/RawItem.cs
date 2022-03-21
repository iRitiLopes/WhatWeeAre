using UnityEngine;
using System;
using Inventory;

namespace Inventory {
    public class RawItem {

        public Guid id;
        public int quantity;


        public RawItem(Guid id, int quantity) {
            this.id = id;
            this.quantity = quantity;
        }

        public RawItem(RawItem item) {
            this.id = item.id;
            this.quantity = item.quantity;
        }

    }
}
