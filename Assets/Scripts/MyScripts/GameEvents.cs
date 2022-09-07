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
            current.ActionPressed();
        }

        if(Input.GetKeyDown(KeyCode.Escape)){
            current.EscPressed();
        }

    }

    public event Action OnActionPressed;
    public void ActionPressed() {
        if (current.OnActionPressed == null) {
            Debug.Log("Tem nada");
        }
        current.OnActionPressed?.Invoke();
    }


    public event Action OnEscPressed;
    public void EscPressed(){
        if(current.OnEscPressed == null) {
            Debug.Log("Tem nada");
        }
        current.OnEscPressed?.Invoke();
    }
}