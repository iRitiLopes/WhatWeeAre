using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    [HideInInspector]
    public Transform Target;

    public Vector2 Offset = new Vector2(0, 3);

	// Use this for initialization
	void Start () {
        Target = GameObject.Find("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        if (Target == null)
            return;

        Vector3 pos = this.transform.position;
        pos.x = Target.position.x + Offset.x;
        pos.y = Target.position.y + Offset.y;

        this.transform.position = pos;
	}
}
