using UnityEngine;

public class PlayerTooltip : Tooltip {

    [SerializeField]
    private Player player;

    [SerializeField]
    private int xOffset = 0;

    [SerializeField]
    private int yOffset = 0;


    private void Update() {
        
    }

    private void FixedUpdate() {
        Vector3 playerPosition = player.transform.position;
        playerPosition.y = playerPosition.y + yOffset;
        playerPosition.x = playerPosition.x + xOffset;
        transform.position = playerPosition;
    }
}
