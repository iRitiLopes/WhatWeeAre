using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPoint : MonoBehaviour {

    void Start() {
        GameManager.unlockLevel(SceneManager.GetActiveScene().name);

        Debug.Log("Setting StartPoint");
        GameManager.setStartPoint(this);
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnDestroy() {
        GameManager.setStartPoint(null);
    }

}
