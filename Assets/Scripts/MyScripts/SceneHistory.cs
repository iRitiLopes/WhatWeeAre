using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHistory : MonoBehaviour {


    public static SceneHistory instance;
    [SerializeField]
    public List<string> sceneHistory = new();  //running history of scenes
                                                             //The last string in the list is always the current scene running

    private void Awake() {
        DontDestroyOnLoad(this);

        if (instance == null) {
            instance = this;
        } else {
            Object.Destroy(gameObject);
        }
    }
    void Start() {
        sceneHistory.Add(SceneManager.GetActiveScene().name);
    }


    public void cleanHistory(){
        sceneHistory.Clear();
        sceneHistory.Add(SceneManager.GetActiveScene().name);
    }

    //Call this whenever you want to load a new scene
    //It will add the new scene to the sceneHistory list
    public void __LoadScene(string newScene) {
        Debug.Log("Loading scene: " + newScene);
        Debug.Log("Current scene: " + SceneManager.GetActiveScene().name);
        if(newScene != "LevelSelection"){
            sceneHistory.Add(newScene);
        }

        if(newScene == "LevelSelection"){
            cleanHistory();

            if(GameManager.gameManagerInstance.endGame){
                //GameManager.gameManagerInstance.firstRun = true;
                //GameManager.gameManagerInstance.endGame = false;
            }else{
                sceneHistory.Add(SceneManager.GetActiveScene().name);
            }
            
        }
        
        SceneManager.LoadScene(newScene);
    }

    public void __LoadScene(string newScene, bool startFromStartPoint) {
        __LoadScene(newScene);
        if (startFromStartPoint) {
            Debug.Log("Start from start point");
            GameManager.gameManagerInstance.startFromStartPoint();
        }
    }

    //Call this whenever you want to load the previous scene
    //It will remove the current scene from the history and then load the new last scene in the history
    //It will return false if we have not moved between scenes enough to have stored a previous scene in the history
    public bool __PreviousScene() {
        bool returnValue = false;
        if (sceneHistory.Count >= 2)  //Checking that we have actually switched scenes enough to go back to a previous scene
        {
            returnValue = true;
            sceneHistory.RemoveAt(sceneHistory.Count - 1);
            SceneManager.LoadScene(sceneHistory[sceneHistory.Count - 1]);
            //GameManager.gameManagerInstance.startFromStartPoint();
        }

        return returnValue;
    }

    public static bool PreviousScene() {
        return instance.__PreviousScene();
    }

    public static void LoadScene(string newScene) {
        instance.__LoadScene(newScene);
    }

    public static void LoadScene(string newScene, bool startFromStartPoint = false) {
        instance.__LoadScene(newScene, startFromStartPoint);
    }

}