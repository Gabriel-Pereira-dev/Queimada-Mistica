using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    private float yaw = 0f;
    private float pitch = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
    
        // xRotation -= mouseY;
        // xRotation = Mathf.Clamp(xRotation, -90f,90f);

        // transform.Rotate(Vector3.up * mouseX);
        // transform.localRotation = Quaternion.Euler(xRotation,0f,0f);
        playerBody.Rotate(Vector3.up * mouseX);

        pitch -= mouseY;
        yaw += mouseX;

        pitch = Mathf.Clamp(pitch, -60f, 90f);

        transform.eulerAngles = new Vector3(pitch,yaw,0f);
    }
}
