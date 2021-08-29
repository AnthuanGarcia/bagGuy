using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float BringForce = 50;
    Rigidbody2D rb;
    Animator animator;
    float lastVel;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
            animator.SetTrigger("bounce");
            rb.AddForce(Vector2.up * Mathf.Abs(lastVel * BringForce));
        }
    }
}
