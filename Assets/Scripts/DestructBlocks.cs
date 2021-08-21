using UnityEngine;

public class DestructBlocks : MonoBehaviour
{
    Rigidbody2D playerRb, blockRb;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        blockRb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            Debug.Log(playerRb.velocity.y.ToString());

            if(playerRb.velocity.y < -27f)
                blockRb.gameObject.SetActive(false);
        }
    }
}
