using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUp : MonoBehaviour
{

    [SerializeField]
    List<PowerUpEffect> powerUpEffects = new();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPowerUp(PowerUpEffect powerUpEffect){
        powerUpEffect.Apply(gameObject);
    }
}