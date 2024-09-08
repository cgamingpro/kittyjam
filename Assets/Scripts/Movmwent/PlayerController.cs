using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : MonoBehaviour
{
    public GameObject camera;
    CharacterController characterController;
    [SerializeField] float speed;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        float inputx = Input.GetAxisRaw("Horizontal");
        float inputz = Input.GetAxisRaw("Vertical");

        Move1(inputx,inputz);
    }

    public void Move1(float inpx,float inpz)
    {
        Vector3 velocity = new Vector3(inpx,0, inpz);
        Vector3 move = camera.transform.right * inpx + camera.transform.forward * inpz;
        characterController.Move(move * speed * Time.deltaTime);
    }
}
