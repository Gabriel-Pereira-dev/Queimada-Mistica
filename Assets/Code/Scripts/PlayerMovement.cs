using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [Header("Player Movement Settings")]
    public CharacterController controller;
    public float jumpHeight = 3f;
    public float speed = 12f;
    Vector3 velocity;
    public float gravity = -9.81f;
    [Header("Ground Check Settings")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    private FMOD.Studio.EventInstance instance;


    // Start is called before the first frame update
    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/sfx_step");
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position,groundDistance,groundMask);

        if(isGrounded && velocity.y < 0){
            velocity.y -= 2f;
        }

        float x = Input.GetAxis("Horizontal");
        // float z = Input.GetAxis("Vertical");
        if(x != 0 && isGrounded){
            if(!IsPlaying(instance)){
                instance.start();
            }
            
        }else{
            instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        
        Vector3 move = transform.right * x;
        // + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded){
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    bool IsPlaying(FMOD.Studio.EventInstance instance) {
        FMOD.Studio.PLAYBACK_STATE state;   
        instance.getPlaybackState(out state);
        return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    }
}
