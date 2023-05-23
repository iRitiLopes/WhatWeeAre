using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateLava : MonoBehaviour
{

    public float speed = 0.5f;
    public float elapsedTime = 0f;

    public float offset = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Animate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Animate(){
        while(true){
            foreach (Transform child in transform)
            {
                child.position += new Vector3(offset, 0, 0);
                offset *= -1;
                
            }
            offset *= -1;
            yield return new WaitForSeconds(speed);
        }
        
    }
}
