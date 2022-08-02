using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GameEvents : MonoBehaviour {
    public static GameEvents current;

    private void Awake() {
        DontDestroyOnLoad(this);
        current = this;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.J)){
            ActionPressed();
        }
        
    }

    public event Action OnActionPressed;
    public void ActionPressed(){
        if(OnActionPressed == null){
            Debug.Log("Tem nada");
        }
        OnActionPressed?.Invoke();
    }
}