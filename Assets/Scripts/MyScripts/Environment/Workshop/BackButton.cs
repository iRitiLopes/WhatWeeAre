using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BackButton : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        gameObject.GetComponent<Button>().onClick.AddListener(Back);
    }

    // Update is called once per frame
    void Update() {

    }

    private void FixedUpdate() {

    }

    public void Back() {
        SceneHistory.PreviousScene();
    }
}
