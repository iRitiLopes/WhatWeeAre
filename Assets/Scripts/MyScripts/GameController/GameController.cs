using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public float LoadDelay = 0.5f;
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
        StartCoroutine(reload());
    }

    IEnumerator reload(){
        yield return new WaitForSeconds(LoadDelay);
        SceneManager.LoadScene(this.scene.name);
    }
}
