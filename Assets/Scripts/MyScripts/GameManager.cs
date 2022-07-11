using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager gameManagerInstance;

    public bool firstRun = true;

    public Vector3 playerPosition;
    readonly List<GameObject> enemies;
    readonly List<GameObject> items;

    public Dictionary<GameObject, GameObject> items2 = new Dictionary<GameObject, GameObject>();

    private void Start() {

    }

    private void Awake() {
        DontDestroyOnLoad(this);

        if (gameManagerInstance == null) {
            gameManagerInstance = this;
        } else {
            Object.Destroy(gameObject);
        }
    }

    public static void save() {
        gameManagerInstance.savePlayerPosition();
    }

    public void savePlayerPosition() {
        var player = FindObjectOfType<Player>();
        if (player == null) {
            return;
        }
        playerPosition = player.playerPosition;
    }

    public static void load() {
        gameManagerInstance.loadPlayerPosition();
    }

    public void loadPlayerPosition() {
        var player = FindObjectOfType<Player>();
        if (player == null) {
            return;
        }
        player.LoadPlayerPosition(playerPosition);
    }

    public static void createItem(Vector3 position, GameObject collectableItem) {
        var collectable = UnityEngine.Object.Instantiate(
            collectableItem,
            parent: GameObject.FindGameObjectWithTag("ItemWrapper").transform) as GameObject;
        collectable.name = "DropableItem_";
        collectable.tag = "Itens";
        collectable.transform.position = position;
    }
}
