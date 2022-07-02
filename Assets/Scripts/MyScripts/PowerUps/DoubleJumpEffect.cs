using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "powerUps/DoubleJump")]
public class DoubleJumpEffect : PowerUpEffect {
    public override void Apply(GameObject target) {
        target.GetComponent<Jump>().enableDoubleJump();
    }
}
