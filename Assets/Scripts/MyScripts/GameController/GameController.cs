using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public float LoadDelay = 0.5f;
    public float actualTime = 0;
    public Scene scene;

    private bool reloading = false;
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

    public void reloadLevel(Action<Collider2D> action, Collider2D other){
        if(!reloading)
            StartCoroutine(reload(action, other));
    }

    public void reloadLevelWithCustomDelay(float delay){
        StartCoroutine(reloadWithCustomDelay(delay));
    }

    IEnumerator reload(Action<Collider2D> action, Collider2D other){
        reloading = true;
        GameManager.gameManagerInstance.Pause();
        yield return new WaitForSeconds(LoadDelay);
        SceneManager.LoadScene(this.scene.name);
        GameManager.gameManagerInstance.Unpause();
        action?.Invoke(other);
        reloading = false;
    }

    IEnumerator reloadWithCustomDelay(float delay){
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(this.scene.name);
    }
}
