using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderHelper : MonoBehaviour {


    List<Notificable> notificables = new();
    public bool isColliding = false;

    public BoxCollider2D boxCollider2D;

    private void Start() {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    

    private void Update() {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));

        if(hit.collider != null){
            isColliding = true;
        } else {
            isColliding = false;
        }
        notifyAll();
    }

    private void OnCollisionEnter2D(Collision2D coll) {
        if(coll.collider.CompareTag("Platform")){
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
