using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invulnerable : MonoBehaviour {
    
    public float invulnerabilityTime = 3.0f;
    public bool isInvulnerable = false;


    public float blinkRatio = 5.0f;

    SpriteRenderer r;

    Color color;

    private void Start() {
        r = GetComponent<SpriteRenderer>();
        Physics2D.IgnoreLayerCollision(8, 9, false);
    }

    private void Update() {

    }

    public IEnumerator invulnerable(){
        isInvulnerable = true;
        Physics2D.IgnoreLayerCollision(8, 9, true);
        var coroutine = blinkCharacter();

        StartCoroutine(coroutine);

        yield return new WaitForSecondsRealtime(this.invulnerabilityTime);
        
        isInvulnerable = false;
        Physics2D.IgnoreLayerCollision(8, 9, false);
    }

    public IEnumerator blinkCharacter(){
        var ratio = invulnerabilityTime / blinkRatio;
        var elapsedTime = 0f;
        while(elapsedTime < invulnerabilityTime){
            r.material.color = new Color(255, 255, 255, 1);
            yield return new WaitForSeconds(ratio);

            r.material.color = Color.white;
            yield return new WaitForSeconds(ratio);

            elapsedTime += 2*ratio;
        }
    }
}