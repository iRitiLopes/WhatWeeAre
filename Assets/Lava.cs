using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour {
    public GameObject[] levels;


    [SerializeField]
    private Camera mainCamera;

    private Vector2 screenBounds;

    [SerializeField]
    Sprite lavaSprite;
    // Start is called before the first frame update
    void Start() {
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        foreach (GameObject obj in levels) {
            loadChildObjects(obj);
        }
    }

    void loadChildObjects(GameObject obj) {
        float objectWitdh = obj.GetComponent<SpriteRenderer>().bounds.size.x;
        int childsNeeded = (int)Mathf.Ceil(screenBounds.x * 4 / objectWitdh);
        GameObject clone = Instantiate(obj) as GameObject;
        for (int i = 0; i <= childsNeeded; i++) {
            GameObject c = Instantiate(clone) as GameObject;
            c.transform.SetParent(obj.transform);
            c.transform.position = new Vector3(objectWitdh * i, obj.transform.position.y, obj.transform.position.z);
            c.name = obj.name + i;
        }
        Destroy(clone);
        Destroy(obj.GetComponent<SpriteRenderer>());
    }

    // Update is called once per frame
    void Update() {

        transform.position = new Vector3(mainCamera.transform.position.x + 5, mainCamera.transform.position.y, mainCamera.transform.position.z);
    }

    void LateUpdate() {
        foreach (GameObject obj in levels) {
            repositionChildObjects(obj);
        }
    }

    void repositionChildObjects(GameObject obj) {
        Transform[] children = obj.GetComponentsInChildren<Transform>();
        if (children.Length > 1) {
            GameObject firstChildren = children[1].gameObject;
            GameObject lastChild = children[children.Length - 1].gameObject;
            float halfObjectWidth = lastChild.GetComponent<SpriteRenderer>().bounds.extents.x;
            if (transform.position.x + screenBounds.x > lastChild.transform.position.x + halfObjectWidth) {
                firstChildren.transform.SetAsLastSibling();
                firstChildren.transform.position = new Vector3(lastChild.transform.position.x + halfObjectWidth * 2, lastChild.transform.position.y, lastChild.transform.position.z);
            } else if (transform.position.x - screenBounds.x < firstChildren.transform.position.x - halfObjectWidth) {
                lastChild.transform.SetAsFirstSibling();
                lastChild.transform.position = new Vector3(firstChildren.transform.position.x - halfObjectWidth * 2, firstChildren.transform.position.y, firstChildren.transform.position.z);
            }
        }
    }
}
