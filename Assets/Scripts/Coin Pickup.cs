using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinSound;
    [SerializeField] int coinValue = 100;

    bool isPickedUp = false;
    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player" && !isPickedUp){
            isPickedUp = true;
            FindObjectOfType<GameSession>().AddScore(coinValue);
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(coinSound,Camera.main.transform.position);
        }
    }
}
