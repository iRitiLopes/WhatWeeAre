using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "powerUps/JumpForceIncrease")]
public class JumpForceIncrease : PowerUpEffect {
    [SerializeField]
    public float amount = 2.87f;

    [SerializeField]
    public AudioClip audioClip;

    public override void Apply(GameObject target, PlayerPowerUp playerPowerUp) {
        target.GetComponent<Jump>().jumpForce = amount;
        target.GetComponent<AudioSource>().PlayOneShot(audioClip);
        playerPowerUp.AddPowerUp(this);
    }
}
