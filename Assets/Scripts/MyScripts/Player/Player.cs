using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour {
    private Rigidbody2D rb;

    Animator an;

    GameControl game;

    PlayerColliderHelper bottomHelper;

    PlayerColliderHelper rightHelper;

    PlayerColliderHelper leftHelper;

    public int lifes = 3;

    public bool isKnocked = false;

    public Vector3 playerPosition;

    // Use this for initialization
    private IEnumerator Start() {
        game = GameObject.Find("Main Camera").GetComponent<GameControl>();

        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();

        bottomHelper = GetComponentsInChildren<PlayerColliderHelper>()[0];
        rightHelper = GetComponentsInChildren<PlayerColliderHelper>()[1];
        leftHelper = GetComponentsInChildren<PlayerColliderHelper>()[2];

        yield return new WaitUntil(() =>
                    GameManager.gameManagerInstance != null);

        GameManager.load();
    }

    private void Awake() {
        //loadPlayerPosition();
    }

    void SavePlayerPosition() {
        playerPosition = new Vector3(
                transform.position.x,
                transform.position.y,
                transform.position.z);
    }

    public void LoadPlayerPosition(Vector3 playerPosition) {
        transform.position = playerPosition;
    }

    private void Update() {
        SavePlayerPosition();
        if (IsDead()) {
            an.SetBool("Dead", true);
            GameObject
                .FindGameObjectWithTag("MainCamera")
                .GetComponent<GameController>()
                .reloadLevel();
            return;
        } else {
            an.SetBool("Dead", false);
        }

        if (Input.GetKey(KeyCode.I)) {
            SceneHistory.LoadScene("Inventory");
        }
    }

    private void FixedUpdate() {
        if (game.IsGameRuning == false) {
            an.SetFloat("Velocity", 0);
            return;
        }
    }

    public void DecreaseLife() {
        if (lifes >= 0) lifes--;
    }

    public float GetAbsRunVelocity() {
        return Mathf.Abs(rb.velocity.x);
    }

    public bool IsDead() {
        return lifes <= 0;
    }

    internal void IncreaseLife(int amount) {
        this.lifes += amount;
    }
}
