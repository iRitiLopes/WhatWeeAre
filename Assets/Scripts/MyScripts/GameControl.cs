﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {
    // Use this for initialization
    public bool IsGameRuning = true;

    public bool[] ItensLevel1 = new bool[2];


    void Start() {


        GameObject startPoint = GameObject.Find("StartPoint");
        if (startPoint == null) {
            throw new UnityException("Start point do not exists!");
        }

        GameObject player = GameObject.Find("Player");
        if (player == null) {
            throw new UnityException("Player do not exists!");
        }

        GameObject ect = GameObject.Find("ExitPoint");
        if (ect == null) {
            throw new UnityException("Exit Point do not exists!");
        }

        player.transform.position =
            new Vector3(startPoint.transform.position.x,
                startPoint.transform.position.y - 1,
                0);


        for (int i = 0; i < ItensLevel1.Length; i++) {
            if (ItensLevel1[i] == true) {
                Destroy(GameObject.Find("Item" + i));
            }
        }
    }

    // Update is called once per frame
    void Update() {
    }
}
