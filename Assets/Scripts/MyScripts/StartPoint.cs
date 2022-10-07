using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPoint : MonoBehaviour {

    void Start() {
        GameManager.unlockLevel(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update() {

    }

}
