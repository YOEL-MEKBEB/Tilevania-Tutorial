using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
 
    [SerializeField] float enemySpeed = 2f;
    Rigidbody2D enemy;
    void Start()
    {
        enemy = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        enemy.velocity = new Vector2(enemySpeed, 0f);
        
    }

    void OnTriggerExit2D(Collider2D other){
        Debug.Log("Entered");
        enemySpeed = -enemySpeed;
        transform.localScale = new Vector2(Mathf.Sign(enemySpeed), 1f);
    }

   
}
