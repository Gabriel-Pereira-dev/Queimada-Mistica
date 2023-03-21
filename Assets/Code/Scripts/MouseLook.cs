using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Camera Settings")]
    public Vector2 limitsXAxis = new Vector2(-60f,60f);
    public Vector2 limitsYAxis = new Vector2(-60f,60f);
    public float mouseSensitivity = 100f;
    // public Transform player;
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
    
        // player.Rotate(Vector3.up * mouseX);

        pitch -= mouseY;
        yaw += mouseX;

        pitch = Mathf.Clamp(pitch, limitsYAxis.x, limitsYAxis.y);
        yaw = Mathf.Clamp(yaw, limitsXAxis.x, limitsXAxis.y);

        transform.eulerAngles = new Vector3(pitch,yaw,0f);
        // player.eulerAngles = new Vector3(0,yaw,0f);
    }
}
