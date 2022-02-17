using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Inventory;

public class GameController : MonoBehaviour
{
    public float LoadDelay = 3f;
    public float actualTime = 0;
    public Scene scene;
    //public ItemDatabase database;
    // Start is called before the first frame update
    void Start()
    {
        //database = gameObject.AddComponent<ItemDatabase>();
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
