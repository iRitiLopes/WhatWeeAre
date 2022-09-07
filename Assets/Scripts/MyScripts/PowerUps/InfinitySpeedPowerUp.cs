using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "powerUps/InfinitySpeedPowerUp")]
public class InfinitySpeedPowerUp : PowerUpEffect {
    [SerializeField]
    public float amount = 1.0f;

    [SerializeField]
    public AudioClip audioClip;


    public override void Apply(GameObject target, PlayerPowerUp playerPowerUp) {
        target.GetComponent<Movement>().IncreaseSpeed(amount);
        target.GetComponent<AudioSource>().PlayOneShot(audioClip);
        playerPowerUp.AddPowerUp(this);
    }
}
