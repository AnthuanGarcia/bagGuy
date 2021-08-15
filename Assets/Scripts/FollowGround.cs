using UnityEngine;

public class FollowGround : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        transform.position = new Vector2(player.position.x, -1.5f);
    }
}
