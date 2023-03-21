using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    public int stamina = 3;

    void TakeDamage(int damage){
        stamina -= damage;
        if(stamina <= 0){
            Destroy(gameObject);
            Debug.Log("Game Over");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        bool isBall = LayerMask.LayerToName(collision.gameObject.layer) == "Ball";

        if(isBall){
            BallScript ball = collision.gameObject.GetComponent<BallScript>();
            if(ball.isBeingThrownByEnemy){
                TakeDamage(1);
            }
        }
    }


}
