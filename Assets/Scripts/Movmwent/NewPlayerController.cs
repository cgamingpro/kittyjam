using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    public GameObject camera;

    private Vector3 moveVector;
    private Vector3 lastMove;
    [SerializeField]private float speed = 8;
    [SerializeField]private float jumpForce = 8;
    [SerializeField]private float gravity = 25;
    private float verticalVelocity;
    private CharacterController controller;

    
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        Vector3 forward = camera.transform.forward;
        Vector3 right = camera.transform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
        moveVector = (forward * inputZ + right * inputX).normalized;
        //moveVector = Vector3.zero;
        //moveVector.x = Input.GetAxis("Horizontal");
        //moveVector.z = Input.GetAxis("Vertical");
        if (controller.isGrounded) 
        {
            verticalVelocity = -1;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
            moveVector = lastMove;
        }
        moveVector.y = 0;
        moveVector.Normalize();
        moveVector.x *= speed;
        moveVector.z *= speed;
        moveVector.y = verticalVelocity;

        controller.Move(moveVector * Time.deltaTime);
        lastMove = moveVector;
        
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!controller.isGrounded && hit.normal.y < 0.1)
        {
            if (Input.GetButtonDown("Jump"))
            {
                Debug.DrawRay(hit.point, hit.normal, Color.red, 2f);
                verticalVelocity = jumpForce;
                float inputX = Input.GetAxis("Horizontal");
                float inputZ = Input.GetAxis("Vertical");

                // Get camera-based movement
                Vector3 forward = camera.transform.forward;
                Vector3 right = camera.transform.right;
                forward.y = 0f;
                right.y = 0f;
                forward.Normalize();
                right.Normalize();

                Vector3 inputDirection = (forward * inputZ + right * inputX).normalized;

                // Wall jump direction: mix between wall normal and player input
                moveVector = (hit.normal + inputDirection).normalized * speed;

                //moveVector = hit.normal * speed;
            }
            
        }


    }
}
