using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderHelper : MonoBehaviour {


    List<Notificable> notificables = new();
    public bool isColliding = false;

    public BoxCollider2D boxCollider2D;

    public bool left = false;
    public bool right = false;
    public bool bottom = false;

    Vector2 direction;

    private void Start() {
        boxCollider2D = GetComponent<BoxCollider2D>();

        if(bottom){
            direction = Vector2.down;
        } else if(left){
            direction = Vector2.left;
        } else if(right){
            direction = Vector2.right;
        }
    }
    

    private void Update() {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, direction, 0.1f, LayerMask.GetMask("Ground"));

        if(hit.collider != null){
            isColliding = true;
        } else {
            isColliding = false;
        }
        notifyAll();
    }

    private void OnCollisionEnter2D(Collision2D coll) {
        if(coll.collider.CompareTag("Platform")){
            Debug.Log("Colliding" + gameObject.name);
            isColliding = true;
            notifyAll();
        }
    }

    private void OnCollisionExit2D(Collision2D coll) {
        if(coll.collider.CompareTag("Platform")){
            isColliding = false;
            notifyAll();
        }
    }

    public void subscribe(Notificable notificable) {
        notificables.Add(notificable);
    }

    void notifyAll(){
        notificables.ForEach( x => x.notify(this.gameObject));
    }
}
