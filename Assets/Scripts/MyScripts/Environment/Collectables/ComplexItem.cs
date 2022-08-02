using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ComplexRadio", menuName = "New Wee/ComplexItem", order = 0)]
public class ComplexItem : ScriptableObject {
    public string itemName;
    public GameObject[] dismantableItems;
    
}
