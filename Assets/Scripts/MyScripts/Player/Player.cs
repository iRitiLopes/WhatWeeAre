using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour {
    private Rigidbody2D rb;

    Animator an;

    [SerializeField]
    PlayerLife playerLife;

    PlayerColliderHelper bottomHelper;

    PlayerColliderHelper rightHelper;

    PlayerColliderHelper leftHelper;

    public int lifes = 3;

    public bool isKnocked = false;

    public Vector3 playerPosition;
    public int damage = 1;

    // Use this for initialization
    private IEnumerator Start() {

        playerPosition = gameObject.transform.position;
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();

        bottomHelper = GetComponentsInChildren<PlayerColliderHelper>()[0];
        rightHelper = GetComponentsInChildren<PlayerColliderHelper>()[1];
        leftHelper = GetComponentsInChildren<PlayerColliderHelper>()[2];

        GameObject startPoint = GameObject.Find("StartPoint");
        transform.position =
            new Vector3(startPoint.transform.position.x,
                startPoint.transform.position.y - 1,
                0);
        playerPosition = transform.position;

        yield return new WaitUntil(() =>
                    GameManager.gameManagerInstance != null);

        GameManager.load();
        //StartCoroutine(SavePlayerPosition());
    }

    private void Awake() {
        lifes = FindObjectOfType<GameManager>().GetLifes();
        playerLife.updateLife(lifes);
    }

    IEnumerator SavePlayerPosition() {
        yield return new WaitForSeconds(5);
        while (true) {
            playerPosition = new Vector3(
                            transform.position.x,
                            transform.position.y,
                            transform.position.z);
        }

    }

    public void savePosition(){
        playerPosition = transform.position;
        GameManager.save();
        Debug.Log(playerPosition);
    }

    public void LoadPlayerPosition(Vector3 playerPosition) {
        transform.position = playerPosition;
    }

    private void Update() {
        //Check if the player is dead
        if (IsDead()) {
            //If the player is dead, the animation is triggered and the level is reloaded
            an.SetBool("Dead", true);
            GameObject
                .FindGameObjectWithTag("MainCamera")
                .GetComponent<GameController>()
                .reloadLevelWithCustomDelay(3);

            //The life is reset
            FindObjectOfType<GameManager>().SetLife(3);
            return;
        } else {
            //If the player is not dead, the animation is set to false
            an.SetBool("Dead", false);
        }

        //If the inventory button is pressed, the player position is saved and the inventory scene is loaded
        if (Input.GetKey(KeyCode.I)) {
            FindObjectOfType<Player>().savePosition();
            SceneHistory.LoadScene("Inventory");
        }
    }

    private void FixedUpdate() {

        if (Input.GetKey(KeyCode.Z)) {
            Vector3 fwd = transform.TransformDirection(Vector3.forward);

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 400, Color.yellow);
        }
    }

    public void DecreaseLife() {

        if (lifes >= 0) lifes--;
        FindObjectOfType<GameManager>().SetLife(lifes);
        playerLife.updateLife(lifes);
    }

    public float GetAbsRunVelocity() {
        return Mathf.Abs(rb.velocity.x);
    }

    public bool IsDead() {
        return lifes <= 0;
    }

    internal void IncreaseLife(int amount) {
        this.lifes += amount;
        FindObjectOfType<GameManager>().SetLife(lifes);
        playerLife.updateLife(lifes);
    }
}
