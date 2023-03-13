using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    public int stamina = 3;

    void TakeDamage(int damage){
        stamina -= damage;
        if(stamina < 0){
            Destroy(gameObject);
            Debug.Log("Game Over");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        bool isBall = collision.collider.CompareTag("Ball");

        if(isBall){
            TakeDamage(1);
        }
    }


}
