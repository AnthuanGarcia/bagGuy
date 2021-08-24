using UnityEngine;
using UnityEngine.Events;

public class DestructBlocks : MonoBehaviour
{
    public UnityEvent activeBlock;

    Rigidbody2D playerRb, blockRb;
    Animator animator;
    int framesPassed = 0;

    const float breakForceDownDash = -27f;
    const float breakForceDash = 6f;

    void Awake()
    {
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        blockRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("destroy_block_uo_down"))
        {
            if(framesPassed > 20)
            {
                blockRb.gameObject.SetActive(false);
                
                if(activeBlock != null)
                    activeBlock.Invoke();
            }

            framesPassed++;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            if(playerRb.velocity.y < breakForceDownDash || playerRb.velocity.x > 6f)
            {
                animator.SetTrigger("Destroy");
            }
        }
    }
}
