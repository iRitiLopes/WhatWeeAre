using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "powerUps/SpeedPowerUp")]
public class SpeedPowerUp : PowerUpEffect {
    [SerializeField]
    public float amount = 1.0f;

    [SerializeField]
    public AudioClip audioClip;

    [SerializeField]
    public float effectSeconds = 10;

    public override void Apply(GameObject target) {
        target.GetComponent<Movement>().IncreaseSpeed(amount, effectSeconds);
        target.GetComponent<AudioSource>().PlayOneShot(audioClip);
    }

}
