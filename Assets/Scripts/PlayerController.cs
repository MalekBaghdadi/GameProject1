using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public static PlayerController Instance;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    public Vector3 playerMoveDirection;
    [SerializeField] Animator animator;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this; 
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        float inputX =Input.GetAxisRaw("Horizontal");
        float inputY =Input.GetAxisRaw("Vertical");
        playerMoveDirection = new Vector3(inputX,inputY).normalized;
        animator.SetFloat("moveX", inputX);
        animator.SetFloat("moveY", inputY);
        if (playerMoveDirection == Vector3.zero)
        {
            animator.SetBool("moving", false);
        }
        else
        {
            animator.SetBool("moving", true);
        }
        {
            
        }

    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(playerMoveDirection.x * moveSpeed, playerMoveDirection.y * moveSpeed);
    }
}
