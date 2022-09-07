using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpawner : MonoBehaviour {

    [SerializeField]
    GameObject spawnDropPrefab;

    [SerializeField]
    float spawnRatePerSecond = 1;

    float accumulatedTime = 0;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        accumulatedTime += Time.deltaTime;

        if(accumulatedTime >= 1/spawnRatePerSecond){
            this.SpawnDrop();
        }
    }

    private void SpawnDrop() {
        accumulatedTime = 0;
        var go = Instantiate(spawnDropPrefab, transform.position, Quaternion.identity);
        go.transform.parent = transform.parent;
    }
}
