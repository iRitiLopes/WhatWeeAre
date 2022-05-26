using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    Animator an;

    SpriteRenderer render;

    GameControl game;

    PlayerColliderHelper bottomHelper;

    PlayerColliderHelper rightHelper;

    PlayerColliderHelper leftHelper;

    public float Velocity = 1f;

    public int lifes = 3;

    public bool isKnocked = false;

    // Use this for initialization
    private IEnumerator Start()
    {
        Debug.Log("Start");
        game = GameObject.Find("Main Camera").GetComponent<GameControl>();

        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();

        bottomHelper = GetComponentsInChildren<PlayerColliderHelper>()[0];
        rightHelper = GetComponentsInChildren<PlayerColliderHelper>()[1];
        leftHelper = GetComponentsInChildren<PlayerColliderHelper>()[2];

        yield return new WaitUntil(() =>
                    GameManager.gameManagerInstance != null);
        loadPlayerPosition();
    }

    private void Awake()
    {
        //loadPlayerPosition();
    }

    void savePlayerPosition()
    {
        PlayerPrefs.SetFloat("playerX", transform.position.x);
        PlayerPrefs.SetFloat("playerY", transform.position.y);
        PlayerPrefs.SetFloat("playerZ", transform.position.z);
    }

    void loadPlayerPosition()
    {
        var x = PlayerPrefs.GetFloat("playerX", -1);
        var y = PlayerPrefs.GetFloat("playerY", -1);
        var z = PlayerPrefs.GetFloat("playerZ", -1);
        if (x != -1 && y != -1 && z != -1)
        {
            Debug.Log(string.Format("{0} {1} {2}", x, y, z));
            transform.position = new Vector3(x, y, z);
        }
    }

    private void Update()
    {
        savePlayerPosition();

        if (isDead())
        {
            an.SetBool("Dead", true);
            GameObject
                .FindGameObjectWithTag("MainCamera")
                .GetComponent<GameController>()
                .reloadLevel();
            return;
        }
        else
        {
            an.SetBool("Dead", false);
        }

        if (Input.GetKey(KeyCode.I))
        {
            SceneHistory.LoadScene("Inventory");
        }

        Vector2 dir = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
        {
            dir.x = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            dir.x = 1;
        }

        if (!isKnocked)
        {
            Vector2 vel = rb.velocity;
            vel.x = dir.x * Velocity;
            rb.velocity = vel;
        }
        else
        {
        }

        an.SetFloat("Velocity", GetAbsRunVelocity());

        if (rb.velocity.x > 0)
        {
            render.flipX = false;
        }
        else if (rb.velocity.x < 0)
        {
            render.flipX = true;
        }
    }

    private void FixedUpdate()
    {
        if (game.IsGameRuning == false)
        {
            an.SetFloat("Velocity", 0);
            return;
        }
    }

    public void decreaseLife()
    {
        if (lifes >= 0) lifes--;
    }

    public float GetAbsRunVelocity()
    {
        return Mathf.Abs(rb.velocity.x);
    }

    public bool isDead()
    {
        return lifes <= 0;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Itens"))
        {
            ItemDisplay itemDisplay =
                coll.gameObject.GetComponent<ItemDisplay>();
            Inventory.addItem(itemDisplay.item.id, 1);
            itemDisplay.collect();
        }
    }
}
