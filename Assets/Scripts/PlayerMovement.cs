using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public MovementController movementController;
    public float speed = 40f;
    Animator animator;

    bool jump = false;
    bool dash = false;
    bool horizontalDash = false;
    float horizontalMove;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

        if(Input.GetButtonDown("Jump"))
            jump = true;

        if(Input.GetButtonDown("Vertical") &&
            Mathf.Approximately(Input.GetAxisRaw("Vertical"), -1))
            dash = true;

        if(Input.GetButtonUp("Vertical"))
            dash = false;

        if(Input.GetKeyDown(KeyCode.M))
            horizontalDash = true;
        
        UpdateState();
    }

    void FixedUpdate()
    {

        movementController.Move(
            horizontalMove * Time.fixedDeltaTime,
            jump,
            dash,
            horizontalDash
        );

        horizontalDash = jump = false;
    }

    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
    }

    private void UpdateState()
    {
        if (Mathf.Approximately(horizontalMove, 0))
            animator.SetBool("isWalking", false);
        else
            animator.SetBool("isWalking", true);

        if(jump) 
        {
            animator.SetBool("isJumping", true);
        }

    }
}
