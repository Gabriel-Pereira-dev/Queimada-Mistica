using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPickUpBall : MonoBehaviour
{
    [Header("Pickup Settings")]
    [SerializeField] Transform holdArea;
    [HideInInspector]public GameObject heldObject;
    private Rigidbody heldObjectRB;
    private Collider heldObjectCollider;

    [Header("Physics Parameters")]
    [SerializeField] float pickupForce = 150.0f;
    [SerializeField] float dropForce = 150.0f;

    // Update is called once per frame
    void Update()
    {
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

    public void PickupObject(GameObject pickObject){
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

    public void ThrowObject(){
        if(heldObject != null){
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
    }
}
