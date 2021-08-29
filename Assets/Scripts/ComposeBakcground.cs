using UnityEngine;

public class ComposeBakcground : MonoBehaviour
{
    public Transform newPosition, background;
    public float speed = 5f;
    [HideInInspector] public static int tilesBackground = 0;
    Vector2 velocity = Vector2.zero;
    bool move = false;
    bool accommodate = false;
    float dist;
    BoxCollider2D colliderTile;

    void Start()
    {
        colliderTile = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        if(move)
        {
            if(!accommodate)

                transform.position = Vector2.Lerp(
                    transform.position,
                    newPosition.position,
                    Time.smoothDeltaTime * speed * 2.25f
                );

            else
                
                transform.position = Vector2.MoveTowards(
                    transform.position,
                    newPosition.position,
                    Time.fixedDeltaTime * speed * 20f
                );

            dist = Vector2.Distance(newPosition.position, transform.position);
            accommodate = dist >= 0f && dist <= 0.1f;

            if(Mathf.Approximately(dist, 0f))
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
