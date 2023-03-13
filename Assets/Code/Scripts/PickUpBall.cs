using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBall : MonoBehaviour
{
    [Header("Pickup Settings")]
    [SerializeField] Transform holdArea;
    private GameObject heldObject;
    private Rigidbody heldObjectRB;
    private Collider heldObjectCollider;

    [Header("Physics Parameters")]
    [SerializeField] private float pickupRange = 5.0f;
    [SerializeField] float pickupForce = 150.0f;
    [SerializeField] float dropForce = 150.0f;
    private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position,transform.TransformDirection(Vector3.forward * pickupRange),Color.green);
        if(Input.GetButtonDown("Fire1")){
            if(heldObject == null){
                RaycastHit hit;
                if(Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward),out hit,pickupRange)){
                    PickupObject(hit.collider.gameObject);
                }
            }else{
                DropObject();
            }
        }
    }

    void MoveObject(){
        if(Vector3.Distance(heldObject.transform.position,holdArea.position) > 0.1f){
            Vector3 moveDirection = holdArea.position - heldObject.transform.position;
            heldObjectRB.AddForce(moveDirection * pickupForce);
        }
    }

    void PickupObject(GameObject pickObject){
        if(pickObject.GetComponent<Rigidbody>()){
            heldObjectRB = pickObject.GetComponent<Rigidbody>();
            heldObjectCollider = pickObject.GetComponent<Collider>();

            heldObjectRB.useGravity = false;
            heldObjectRB.drag = 10;
            heldObjectRB.constraints = RigidbodyConstraints.FreezeRotation;
            heldObjectCollider.enabled = false;

            heldObjectRB.transform.parent = holdArea;
            heldObject = pickObject;
        }
    }

    void DropObject(){
      
        heldObjectRB.AddForce(transform.forward * dropForce,ForceMode.Impulse);
        heldObjectRB.useGravity = true;
        heldObjectRB.drag = 1;
        heldObjectRB.constraints = RigidbodyConstraints.None;
        heldObjectCollider.enabled = true;

        heldObjectRB.transform.parent = null;
        heldObject = null;
        heldObjectCollider = null;
        }
}
