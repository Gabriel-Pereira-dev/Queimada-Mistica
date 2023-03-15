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
    public LayerMask whatIsBall;

    [Header("Physics Parameters")]
    [SerializeField] private float pickupRange = 5.0f;
    [SerializeField] float pickupForce = 150.0f;
    [SerializeField] float dropForce = 150.0f;
    [SerializeField] float maxDistance = 5.0f;

    RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")){
            if(heldObject == null){
                // RaycastHit hit;
                // Physics.CheckSphere(transform.position,pickupRange,whatIsBall);
                // Physics.SphereCast(transform.position,pickupRange,transform.TransformDirection(Vector3.forward),out hit,Vector3.Distance(transform.position,transform.TransformDirection(Vector3.forward) * pickupRange),whatIsBall);
                // Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward),out hit,pickupRange)
                if(Physics.SphereCast(transform.position,pickupRange,transform.forward,out hit,maxDistance ,whatIsBall)){
                    PickupObject(hit.collider.gameObject);
                }
            }else{
                ThrowObject();
            }
        }

        if(heldObject != null){
            MoveObject();
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

            heldObject.GetComponent<BallScript>().SetBallPickupedState();
        }
    }

    void ThrowObject(){
        heldObject.GetComponent<BallScript>().SetBallThrowState();
        heldObjectRB.AddForce(transform.forward * dropForce,ForceMode.Impulse);
        heldObjectRB.useGravity = true;
        heldObjectRB.drag = 1;
        heldObjectRB.constraints = RigidbodyConstraints.None;
        heldObjectCollider.enabled = true;

        heldObjectRB.transform.parent = null;
        heldObject = null;
        heldObjectCollider = null;
    }

    private void OnDrawGizmosSelected(){
        // RaycastHit hit;
         // Physics.CheckSphere(transform.position,pickupRange,whatIsBall);
        // Physics.SphereCast(transform.position,pickupRange,transform.TransformDirection(Vector3.forward),out hit,Vector3.Distance(transform.position,transform.TransformDirection(Vector3.forward) * pickupRange),whatIsBall);
        // Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward),out hit,pickupRange)
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position + transform.forward  * maxDistance, pickupRange );
        // Gizmos.color = Color.green;
        // Gizmos.DrawRay(transform.position,transform.TransformDirection(Vector3.forward * pickupRange));
    }
}
