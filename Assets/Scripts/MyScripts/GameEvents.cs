using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GameEvents : MonoBehaviour {
    public static GameEvents current;

    private void Awake() {
        current = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.J)) {
            ActionPressed();
        }

    }

    public event Action OnActionPressed;
    public void ActionPressed() {
        if (current.OnActionPressed == null) {
            Debug.Log("Tem nada");
        }
        current.OnActionPressed?.Invoke();
    }
}