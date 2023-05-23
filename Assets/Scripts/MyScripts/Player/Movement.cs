using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

public class Movement : MonoBehaviour {

    SpriteRenderer render;
    public Rigidbody2D rb;
    private Animator an;
    public bool isKnocked = false;
    public float Velocity = 1f;

    public float m_ScreenX = 0.22f;
    public float initialMScreenX = 0.22f;
    private PlayerColliderHelper leftHelper;
    private PlayerColliderHelper rightHelper;
    CinemachineVirtualCamera cinemachineVirtualCamera;

    [SerializeField]
    private bool enableWalkOnWallEffect = false;

    private void Start() {
        render = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        cinemachineVirtualCamera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        initialMScreenX = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX;

        leftHelper = GameObject.FindGameObjectWithTag("PlayerColliderLeft").GetComponent<PlayerColliderHelper>();
        rightHelper = GameObject.FindGameObjectWithTag("PlayerColliderRight").GetComponent<PlayerColliderHelper>();
    }

    internal void IncreaseSpeed(float amount, float seconds) {
        StartCoroutine(IncreaseAndDecreaseSpeed(amount, seconds));
    }

    internal void IncreaseSpeed(float amount) {
        Velocity += amount;
    }

    private IEnumerator IncreaseAndDecreaseSpeed(float amount, float seconds) {
        Velocity += amount;
        yield return new WaitForSeconds(seconds);
        Velocity -= amount;
    }

    public bool isOnWall() {
        return leftHelper.isColliding || rightHelper.isColliding;
    }

    private void WalkOnWall() {
        if (enableWalkOnWallEffect == false) {
            return;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(0, Velocity);
            transform.rotation = Quaternion.Euler(0, 0, 90);
        } else {
            rb.gravityScale = 1;
            transform.rotation = Quaternion.Euler(0, 0, z: 0);
        }
    }


    private void Update() {
        if(GameManager.gameManagerInstance.isPaused) {
            rb.velocity = Vector2.zero;
            return;
        }
        if (FindObjectOfType<GameManager>().isPaused) {
            return;
        }

        if (isOnWall()) {
            Debug.Log("On wall");
            WalkOnWall();
        } else {
            rb.gravityScale = 1;
        }

        Vector2 dir = Vector2.zero;
        if (Input.GetKey(KeyCode.A)) {
            dir.x = -1;
            var val = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX;
            if (val < 0.5) {
                StartCoroutine(changeValueOverTime(val, 1 - initialMScreenX, 0.5f));
                cinemachineVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = m_ScreenX;
            }
        } else if (Input.GetKey(KeyCode.D)) {
            dir.x = 1;
            var val = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX;
            if (val > 0.5) {
                StartCoroutine(changeValueOverTime(val, 1 - initialMScreenX, 0.5f));
                cinemachineVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = initialMScreenX;
            }
        }

        if (!isKnocked) {
            Vector2 vel = rb.velocity;
            vel.x = dir.x * Velocity;
            rb.velocity = vel;
        } else {
        }

        an.SetFloat("Velocity", GetAbsRunVelocity());

        if (rb.velocity.x > 0) {
            render.flipX = false;
        } else if (rb.velocity.x < 0) {
            render.flipX = true;
        }
    }

    public float GetAbsRunVelocity() {
        return Mathf.Abs(rb.velocity.x);
    }

    //slowly increase float from x to y in s seconds
    IEnumerator changeValueOverTime(float fromVal, float toVal, float duration) {
        float counter = 0f;

        while (counter < duration) {
            if (Time.timeScale == 0)
                counter += Time.unscaledDeltaTime;
            else
                counter += Time.deltaTime;

            float val = Mathf.Lerp(fromVal, toVal, counter / duration);
            m_ScreenX = val;
            yield return null;
        }
    }
}