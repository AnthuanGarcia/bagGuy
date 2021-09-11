using UnityEngine;

public class SpawnLetters : MonoBehaviour
{
    public Color colorTint;
    public float maxDistance;
    public float maxCamDist;

    SpriteRenderer sprite;
    Color initColor;
    Transform player;

    float initPos;
    float dist, rel;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        initPos = transform.position.y;
        initColor = sprite.color;
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector2.Distance(player.position, transform.position);

        transform.position = new Vector2(transform.position.x, initPos + Mathf.Sin(dist));
        sprite.color = Color.Lerp(colorTint, initColor, dist/maxCamDist);
    }
}
