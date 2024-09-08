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
    
    private Vector3 verticalvelocity;
    private bool isGrounded;
    public Transform groundCheckpoint;
    public float groundCheckpointRadius = 0.4f;
    public LayerMask groundMask;       // What is considered ground
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheckpoint.position, groundCheckpointRadius, groundMask);

        if (isGrounded && verticalvelocity.y < 0)
        {
           verticalvelocity.y = -2f; // Reset the velocity when grounded
        }

        float inputx = Input.GetAxis("Horizontal");
        float inputz = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            verticalvelocity.y = Mathf.Sqrt(maxjumpHeight * -2f * gravity);
        }
       
        verticalvelocity.y += gravity * Time.deltaTime;

        characterController.Move(verticalvelocity * Time.deltaTime);
        
        Move1(inputx,inputz);
    }


    public void Move1(float inpx,float inpz)
    {
        Vector3 velocity = new Vector3(inpx,0, inpz);
        Vector3 move = camera.transform.right * inpx + camera.transform.forward * inpz;
        characterController.Move(move * speed * Time.deltaTime);
    }
    

}
