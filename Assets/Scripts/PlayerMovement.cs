using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static bool canMove = true;
    public MovementController movementController;
    public float speed = 40f;
    
    Animator animator;
    bool jump = false;
    bool dash = false;
    bool horizontalDash = false;
    float horizontalMove;

    void Awake()
    {
        if(!canMove) canMove = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
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
        animator.SetBool("isWalking", !Mathf.Approximately(horizontalMove, 0));

        if(jump) 
        {
            animator.SetBool("isJumping", true);
        }

    }
}
