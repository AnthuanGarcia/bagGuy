using UnityEngine;

public class ComposeBakcground : MonoBehaviour
{
    [HideInInspector] public static int tilesBackground = 0;

    public Transform newPosition, background;
    public float speed = 5f;

    bool move = false;
    float dist;
    bool comeback = false;
    Vector2 initialPos;

    BoxCollider2D colliderTile;
    SpriteRenderer sprite;
    WayPointFollower wayPointFollower;

    void Start()
    {
        colliderTile = GetComponent<BoxCollider2D>();
        wayPointFollower = GetComponent<WayPointFollower>();
        sprite = GetComponent<SpriteRenderer>();
        initialPos = transform.position;
    }

    void FixedUpdate()
    {
        if(move)
        {
            Vector2 toPosition;

            if(comeback) toPosition = initialPos;
            else toPosition = newPosition.position;

            if(dist > 0f && dist <= 0.5f)

                transform.position = Vector2.MoveTowards(
                    transform.position,
                    toPosition,
                    Time.deltaTime * speed * 15f
                );

            else

                transform.position = Vector2.Lerp(
                    transform.position,
                    toPosition,
                    Time.smoothDeltaTime * speed * 2.25f
                );

            dist = Vector2.Distance(toPosition, transform.position);

            if(Mathf.Approximately(dist, 0f) && !comeback)
            {
                move = comeback = false;
                tilesBackground++;
                sprite.sortingLayerName = "Background";
                GetComponentsInChildren<SpriteRenderer>()[1].enabled = false;
                transform.SetParent(background.transform);
            }
            else if(Mathf.Approximately(dist, 0f) && comeback)
            {
                if(wayPointFollower != null) wayPointFollower.enabled = true;
                move = comeback = false;
                colliderTile.enabled = true;
            }

        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player") && !move)
        {
            if(wayPointFollower != null) 
                wayPointFollower.enabled = false;

            move = true;
            colliderTile.enabled = false;
        }
    }

    public void TileComeback()
    {
        if(Mathf.Approximately(dist, 0))
            return;
        
        comeback = true;
    }
}
