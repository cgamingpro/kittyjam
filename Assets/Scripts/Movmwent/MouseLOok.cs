using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MouseLOok : MonoBehaviour
{

    public float mouseSenstivity = 20f;
    public Transform PlayerYroot;
    public Transform PlayerXroot;
    private float xRotation = 0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSenstivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSenstivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        PlayerXroot.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        PlayerYroot.Rotate(Vector3.up * mouseX);
    }
}
