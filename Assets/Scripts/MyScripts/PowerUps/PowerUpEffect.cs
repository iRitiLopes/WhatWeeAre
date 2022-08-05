using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpEffect : ScriptableObject {
    public string powerUpName;

    public Sprite sprite;
    public abstract void Apply(GameObject target, PlayerPowerUp playerPowerUp);
}
