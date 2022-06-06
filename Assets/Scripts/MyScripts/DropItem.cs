using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public GameObject collectableItem;

    [Range(0, 1)]
    public float odds;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void drop(Vector3 position)
    {
        if (isLucky())
        {
            GameManager.createItem(
                position: new Vector3(position.x, position.y, 0),
                collectableItem: collectableItem);
        }
    }

    private bool isLucky()
    {
        System.Random rand = new System.Random();
        var value = rand.NextDouble();
        return odds >= value;
    }
}
