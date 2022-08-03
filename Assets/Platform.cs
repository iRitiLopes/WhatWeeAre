using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Platform : MonoBehaviour {
    public float speed = 1.0f;

    public float maxHeight;
    public float minHeight;

    bool goDown = false;    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        if(goDown){
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, minHeight, transform.position.z), speed * Time.deltaTime);
        }else{
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, maxHeight, transform.position.z), speed * Time.deltaTime);
        }
        
        if(transform.position.y >= maxHeight){
            goDown = true;
        }

        if(transform.position.y <= minHeight){
            goDown = false;
        }
    }
}
