using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "powerUps/LifePowerUp")]
public class LifePowerUp : PowerUpEffect
{
    [SerializeField]
    public int amount = 3;

    [SerializeField]
    public AudioClip audioClip;

    public override void Apply(GameObject target, PlayerPowerUp playerPowerUp) {
        target.GetComponent<Player>().IncreaseLife(amount);
        target.GetComponent<AudioSource>().PlayOneShot(audioClip);
    }
}
