using UnityEngine;

public class ComposeBakcground : MonoBehaviour
{
    public Transform newPosition, background;
    public float speed = 5f;
    [HideInInspector] public static int tilesBackground = 0;
    bool move = false;
    BoxCollider2D colliderTile;

    void Start()
    {
        colliderTile = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        if(move)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                newPosition.position,
                Time.fixedDeltaTime * speed
            );

            if(Mathf.Approximately(Vector2.Distance(newPosition.position,transform.position), 0f))
            {
                move = false;
                transform.SetParent(background.transform);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player") && !move)
        {
            move = true;
            tilesBackground++;
            colliderTile.enabled = false;
        }
    }
}
