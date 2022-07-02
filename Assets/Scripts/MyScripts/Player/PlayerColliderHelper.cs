using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderHelper : MonoBehaviour {


    List<Notificable> notificables = new();
    public bool isColliding = false;

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
