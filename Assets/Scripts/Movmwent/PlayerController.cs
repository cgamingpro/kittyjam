using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : MonoBehaviour
{
    public GameObject camera;
    CharacterController characterController;
    [SerializeField] float speed;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float maxjumpHeight = 1.5f;
    public float maxWalljumpHeight = 5f;
    public float wallJumpForce = 8f;

    private Vector3 verticalvelocity;
    private bool isTouchingWall;
    private bool isGrounded;
    private bool hasJumped = false;
   
    private bool canWallJump = true;  
    [SerializeField] float wallJumpCooldown = 2f;  
    private float wallJumpTimer = 0f;

    public Transform wallCheck;
    public Transform groundCheckpoint;
    public float wallDistance = 0.5f;
    public float groundCheckpointRadius = 0.4f;
    public LayerMask groundMask;       // What is considered ground

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        isTouchingWall = Physics.CheckSphere(wallCheck.position, wallDistance, groundMask);
        isGrounded = Physics.CheckSphere(groundCheckpoint.position, groundCheckpointRadius, groundMask);

        if (isGrounded && verticalvelocity.y < 0)
        {
           verticalvelocity.y = -2f; // Reset the velocity when grounded
            hasJumped = false;
            canWallJump = true;
            wallJumpTimer = 0f;
        }

        if (!canWallJump)
        {
            wallJumpTimer += Time.deltaTime;
            if (wallJumpTimer >= wallJumpCooldown)
            {
                canWallJump = true; // Cooldown bittišinde tekrar wall jump yapabilir
                wallJumpTimer = 0f;
            }
        }

        float inputx = Input.GetAxis("Horizontal");
        float inputz = Input.GetAxis("Vertical");

        
        
           
        
            
        Vector3 move = camera.transform.right * inputx + camera.transform.forward * inputz;

        move.y = 0;

        characterController.Move(move * speed * Time.deltaTime);
        

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            verticalvelocity.y = Mathf.Sqrt(maxjumpHeight * -2f * gravity);
            
        }

        if (Input.GetButtonDown("Jump") && isTouchingWall && !isGrounded && !hasJumped && canWallJump)
        {
            
            verticalvelocity.y = Mathf.Sqrt(maxWalljumpHeight * -2f * gravity);

            Vector3 wallJumpDirection = -camera.transform.forward;
            characterController.Move(wallJumpDirection * wallJumpForce * Time.deltaTime);
            
            hasJumped = true;
            canWallJump = false;
            wallJumpTimer = 0;
        }

        

        verticalvelocity.y += gravity * Time.deltaTime;

        characterController.Move(verticalvelocity * Time.deltaTime);
        
    }


    





    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (characterController.collisionFlags == CollisionFlags.Sides)
        {
            Debug.DrawRay(hit.point, hit.normal, Color.red, 2f);
        }
    }
}
