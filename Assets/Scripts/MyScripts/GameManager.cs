using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerInstance;

    public bool firstRun = true;

    private void Start()
    {
        Debug.Log(firstRun);
        if (gameManagerInstance != null && gameManagerInstance.firstRun)
        {
            Debug.Log("FirstRun");
            PlayerPrefs.DeleteKey("playerX");
            PlayerPrefs.DeleteKey("playerY");
            PlayerPrefs.DeleteKey("playerZ");
            gameManagerInstance.firstRun = false;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (gameManagerInstance == null)
        {
            gameManagerInstance = this;
        }
        else
        {
            Object.Destroy (gameObject);
        }
    }
}
