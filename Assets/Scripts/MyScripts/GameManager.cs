using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager gameManagerInstance;

    public bool firstRun = true;

    public bool isPaused = false;

    public Vector3 playerPosition;

    readonly Dictionary<string, bool> enemies = new();
    public Dictionary<string, bool> items = new();
    public Dictionary<string, bool> pwups = new();

    public Dictionary<string, bool> dialogues = new();

    public int playerLife = 3;

    public Dictionary<string, bool> levelsCompleted = new();

    StartPoint startPoint;
    public bool endGame = false;

    private void Start() { }

    private void Awake() {
        DontDestroyOnLoad(this);
        if (gameManagerInstance == null) {
            gameManagerInstance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public static void unlockLevel(string level) {
        
        if (!gameManagerInstance.levelsCompleted.ContainsKey(level)) {
            gameManagerInstance.levelsCompleted.Add(level, true);
        } else {
            gameManagerInstance.levelsCompleted[level] = true;
        }
    }

    public static bool isLevelReached(string level) {
        return gameManagerInstance.levelsCompleted.GetValueOrDefault(level, false);
    }

    public void CollectItem(ItemDisplay itemDisplay) {
        items.Add(itemDisplay.hash(), true);
    }

    public void CollectItem(PowerUpDisplay itemDisplay) {
        items.Add(itemDisplay.hash(), true);
    }

    public void CollectItem(ComplexItemDisplay itemDisplay) {
        items.Add(itemDisplay.hash(), true);
    }

    public bool AlreadyCollected(ItemDisplay itemDisplay) {
        return items.GetValueOrDefault(itemDisplay.hash(), false);
    }

    public bool AlreadyCollected(ComplexItemDisplay itemDisplay) {
        return items.GetValueOrDefault(itemDisplay.hash(), false);
    }

    public bool AlreadyCollected(PowerUpDisplay itemDisplay) {
        return items.GetValueOrDefault(itemDisplay.hash(), false);
    }

    public void FinishDialogue(string dialogueName) {
        Debug.Log("GameManager#FinishDialogue " + dialogueName);
        dialogues.Add(dialogueName, true);
    }

    public bool AlreadyDialogue(string dialogueName) {
        Debug.Log("GameManager#AlreadyDialogue " + dialogueName);
        return dialogues.GetValueOrDefault(dialogueName, false);
    }

    public void KillEnemy(Enemy enemy) {
        enemies.Add(enemy.hash(), true);
    }

    public bool EnemyAlreadyDead(Enemy enemy) {
        return enemies.GetValueOrDefault(enemy.hash(), false);
    }

    public void Pause() {
        isPaused = true;
    }

    public void Unpause() {
        isPaused = false;
    }

    public static void save() {
        gameManagerInstance.savePlayerPosition();
    }

    public void savePlayerPosition() {
        var player = FindObjectOfType<Player>();
        if (player == null) {
            return;
        }
        Debug.Log(player.transform.position);
        playerPosition = player.transform.position;
    }

    public static void load() {
        if (!gameManagerInstance.firstRun)
            gameManagerInstance.loadPlayerPosition();
        gameManagerInstance.firstRun = false;
    }

    public void loadPlayerPosition() {
        var player = FindObjectOfType<Player>();
        if (player == null) {
            return;
        }
        player.LoadPlayerPosition(playerPosition);
    }

    public static GameObject createItem(Vector3 position, GameObject collectableItem) {
        var collectable =
            Instantiate(
                collectableItem,
                parent: GameObject.FindGameObjectWithTag("ItemWrapper").transform
            ) as GameObject;
        collectable.name = "DropableItem_";
        collectable.tag = "Itens";
        collectable.transform.position = position;
        return collectable;
    }

    internal void SetLife(int lifes) {
        this.playerLife = lifes;
    }

    internal int GetLifes() {
        return this.playerLife;
    }

    public void loadScene() { }

    public void startFromStartPoint() {
        StartCoroutine(gameManagerInstance.waitStartPoint());
    }

    IEnumerator waitStartPoint(){
        yield return new WaitUntil(() => startPoint != null);
        Debug.Log("StartPoint");
        gameManagerInstance.playerPosition = new Vector3(
            startPoint.transform.position.x, 
            startPoint.transform.position.y, 
            0);
    }

    internal static void setStartPoint(StartPoint startPoint) {
        gameManagerInstance.startPoint = startPoint;
    }

    internal void finishGame() {
        gameManagerInstance.levelsCompleted["Level1Scene"] = true;
        gameManagerInstance.levelsCompleted["Level2Scene"] = true;
        gameManagerInstance.levelsCompleted["Level3Scene"] = true;
        gameManagerInstance.levelsCompleted["Level4Scene"] = true;
 
        SceneHistory.instance.cleanHistory();
        this.endGame = true;
    }
}
