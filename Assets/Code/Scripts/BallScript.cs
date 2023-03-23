using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Layer Settings")]
    public LayerMask whatIsPlayerSide;
    public LayerMask whatisEnemySide;
    [Header("Ball States")]
    public bool isOnPlayerSide;
    public bool isOnEnemySide;
    public bool isPickuped;
    public bool isBeingThrownByPlayer;
    public bool isBeingThrownByEnemy;
    
    void OnCollisionEnter(Collision collision)
    {
        var layer = collision.gameObject.layer;
        int layerEnemy = Mathf.RoundToInt(Mathf.Log(whatisEnemySide.value, 2));;
        int layerPlayer = Mathf.RoundToInt(Mathf.Log(whatIsPlayerSide.value, 2));;

        if(layer == layerEnemy){
            isOnPlayerSide = false;
            isOnEnemySide = true;
            isPickuped = false;
            isBeingThrownByPlayer = false;
            isBeingThrownByEnemy = false;

            return;
        }
        if(layer == layerPlayer){
            isOnPlayerSide = true;
            isOnEnemySide = false;
            isPickuped = false;
            isBeingThrownByPlayer = false;
            isBeingThrownByEnemy = false;
            
            return;
        }
    }

    public void SetBallPlayerThrowState(){
        FMODUnity.RuntimeManager.PlayOneShot("event:/sfx_bola_lancamento");
        isOnEnemySide = false;
        isOnPlayerSide = false;
        isPickuped = false;
        isBeingThrownByPlayer = true;
        isBeingThrownByEnemy = false;
    }

    public void SetBallEnemyThrowState(){
        FMODUnity.RuntimeManager.PlayOneShot("event:/sfx_bola_lancamento");
        isOnEnemySide = false;
        isOnPlayerSide = false;
        isPickuped = false;
        isBeingThrownByPlayer = false;
        isBeingThrownByEnemy = true;
    }

    public void SetBallPickupedState(){
        isOnEnemySide = false;
        isOnPlayerSide = false;
        isPickuped = true;
        isBeingThrownByPlayer = false;
        isBeingThrownByEnemy = false;
    }
}
