using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameComplete : MonoBehaviour
{
    public GameManager gameManager;
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player") && !other.isTrigger){
            StartCoroutine(gameManager.CompleteGame());
        }
    }
}
