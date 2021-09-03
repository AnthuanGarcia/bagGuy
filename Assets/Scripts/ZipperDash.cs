using System.Collections;
using UnityEngine;

public class ZipperDash : MonoBehaviour
{
    public float m_Force = 300f;
    public float m_VerticalForce = 300f;
    public float m_TimeImpulse = 0.5f;

    Rigidbody2D playerRb;
    bool dashing = false;
    bool once = false;
    float originalGravity, sign;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        originalGravity = playerRb.gravityScale;
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if(dashing)
        {
            playerRb.velocity = Vector2.right * sign * m_Force;
        }
        else if(once)
        {
            playerRb.gravityScale = originalGravity;
            once = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            sign = playerRb.velocity.x >= 0 ? 1 : -1;
            playerRb.gravityScale = 0.25f;
            animator.SetTrigger("boost");
            StartCoroutine(ImpulseTime());
        }
    }

    private IEnumerator ImpulseTime()
    {
        dashing = true;
        yield return new WaitForSeconds(m_TimeImpulse);
        once = true;
        dashing = false;
    }
}
