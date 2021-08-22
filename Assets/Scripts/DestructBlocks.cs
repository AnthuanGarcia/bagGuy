using UnityEngine;

public class DestructBlocks : MonoBehaviour
{
    Rigidbody2D playerRb, blockRb;
    Animator animator;
    int framesPassed = 0;

    // Start is called before the first frame update
    void Start()
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
                blockRb.gameObject.SetActive(false);

            framesPassed++;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            if(playerRb.velocity.y < -27f)
            {
                animator.SetTrigger("Destroy");
            }
        }
    }
}
