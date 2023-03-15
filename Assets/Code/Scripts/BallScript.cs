using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    // Start is called before the first frame update
    public LayerMask whatIsPlayerSide,whatisEnemySide;
    public bool isOnPlayerSide;
    public bool isOnEnemySide;
    public bool isPickuped;
    public bool isBeingThrown;

    
    void OnCollisionEnter(Collision collision)
    {
        isOnEnemySide = false;
        isOnPlayerSide = false;

        var layer = collision.gameObject.layer;
        int layerEnemy = Mathf.RoundToInt(Mathf.Log(whatisEnemySide.value, 2));;
        int layerPlayer = Mathf.RoundToInt(Mathf.Log(whatIsPlayerSide.value, 2));;

        if(layer == layerEnemy){
            isOnEnemySide = true;
            isPickuped = false;
            isBeingThrown = false;
            return;
        }
        if(layer == layerPlayer){
            isOnPlayerSide = true;
            isPickuped = false;
            isBeingThrown = false;
            return;
        }
    }

    public void SetBallThrowState(){
        isOnEnemySide = false;
        isOnPlayerSide = false;
        isPickuped = false;
        isBeingThrown = true;
    }

    public void SetBallPickupedState(){
        isOnEnemySide = false;
        isOnPlayerSide = false;
        isPickuped = true;
        isBeingThrown = false;
    }
}
