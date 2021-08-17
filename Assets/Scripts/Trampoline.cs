using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float BringForce = 50;
    Rigidbody2D rb;
    float lastVel;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            lastVel = rb.velocity.y;
        }        
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            rb.AddForce(new Vector2(0f, Mathf.Abs(lastVel * BringForce)));
        }
    }
}
