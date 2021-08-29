using UnityEngine;

public class FollowGround : MonoBehaviour
{
    public Transform player;
    public float axisY = -1.5f;

    void Update()
    {
        transform.position = new Vector2(player.position.x, axisY);
    }
}
