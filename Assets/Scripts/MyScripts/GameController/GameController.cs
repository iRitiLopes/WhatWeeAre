using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public float LoadDelay = 3f;
    public float actualTime = 0;
    public Scene scene;
    // Start is called before the first frame update
    void Start()
    {
        this.scene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void reloadLevel(){
        if(actualTime < LoadDelay){
            actualTime += Time.deltaTime;
            return;
        }
        SceneManager.LoadScene(this.scene.name);
    }
}
