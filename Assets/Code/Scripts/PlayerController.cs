using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 5f;
    public float mouseSensitivity = 1f;
    
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ControllerMovement();
        MouseMovement();
        
        
    }

    private void ControllerMovement(){
        float tempo = Time.deltaTime;
        float controllerHorizontalAxis = Input.GetAxis("Horizontal") * tempo * moveSpeed;
        float controllerVerticalAxis = Input.GetAxis("Vertical") * tempo * moveSpeed;
        Vector3 movement = new Vector3(controllerHorizontalAxis,0f, controllerVerticalAxis);
        transform.position += movement;
        
    }


    private void MouseMovement(){
        // float tempo = Time.deltaTime;
        float mouseHorizontalAxis = Input.GetAxisRaw("Mouse X")  * mouseSensitivity;
        float mouseVerticalAxis = Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

        var eulerAngles = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(eulerAngles.x - mouseVerticalAxis,eulerAngles.y - mouseHorizontalAxis, eulerAngles.z);
        // cameraTransform.localRotation = Quaternion.Euler(cameraTransform.localRotation.eulerAngles + new Vector3(0f,mouseInput.y,0f));
    }


}
