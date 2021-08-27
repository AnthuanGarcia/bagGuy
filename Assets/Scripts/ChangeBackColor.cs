using UnityEngine;

public class ChangeBackColor : MonoBehaviour
{
    public GameObject player;
    public Color startColor;
    public Color endColor;
    public float maxCamDist = 0;
    public bool changeColorPlayer = false;

    float dist = 0;
    bool pass = false;
    Transform playerPos = null;
    SpriteRenderer playerSprite = null;

    void Awake()
    {
        playerPos = player.transform;
        
        if(changeColorPlayer)
            playerSprite = player.GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if(!pass)
        {
            dist = Vector2.Distance(playerPos.position, transform.position);
            Camera.main.backgroundColor = Color.Lerp(startColor, endColor, dist/maxCamDist);

            if(changeColorPlayer)
                playerSprite.color = Color.Lerp(endColor, startColor, dist/maxCamDist);
        }
        else
        {
            Camera.main.backgroundColor = startColor;
            if(changeColorPlayer) playerSprite.color = endColor;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            pass = !pass;
        }
    }
}
