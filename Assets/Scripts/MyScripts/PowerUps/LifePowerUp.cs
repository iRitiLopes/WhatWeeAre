using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "powerUps/LifePowerUp")]
public class LifePowerUp : PowerUpEffect
{
    [SerializeField]
    public int amount = 3;
    public override void Apply(GameObject target) {
        target.GetComponent<Player>().increaseLife(amount);
    }
}
