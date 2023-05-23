using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "powerUps/DoubleDamage")]
public class DoubleDamage : PowerUpEffect {
    [SerializeField]
    public int amount = 4;

    [SerializeField]
    public AudioClip audioClip;

    public override void Apply(GameObject target, PlayerPowerUp playerPowerUp) {
        target.GetComponent<Player>().damage = amount;
        target.GetComponent<AudioSource>().PlayOneShot(audioClip);
        playerPowerUp.AddPowerUp(this, 120);
    }
}
