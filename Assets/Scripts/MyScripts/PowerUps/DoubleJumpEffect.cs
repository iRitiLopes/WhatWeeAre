using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "powerUps/DoubleJump")]
public class DoubleJumpEffect : PowerUpEffect {

    [SerializeField]
    public AudioClip audioClip;

    public override void Apply(GameObject target, PlayerPowerUp playerPowerUp) {
        target.GetComponent<Jump>().enableDoubleJump();
        target.GetComponent<AudioSource>().PlayOneShot(audioClip);
        playerPowerUp.AddPowerUp(this);
    }
}
