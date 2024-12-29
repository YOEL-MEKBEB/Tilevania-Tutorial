using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
   
    Vector2 moveInput;
    [SerializeField] float playerSpeed = 2;

    Rigidbody2D player;
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
    }

    void OnMove(InputValue value){
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void Run(){
        Vector2 playerVelocity = new Vector2(playerSpeed * moveInput.x, player.velocity.y); // gets only the x velocity of moveInput
        player.velocity =  playerVelocity;
    }
    
    void FlipSprite(){
        // Mathf.Sign returns 1 if positive, -1 if negative
        // Mathf.Epsilon is a really small number close to zero but not zero;
        bool playerHasHorizontalSpeed = Mathf.Abs(player.velocity.x) > Mathf.Epsilon;

        if(playerHasHorizontalSpeed){
            transform.localScale = new Vector2(Mathf.Sign(player.velocity.x), 1f);
        }
        
    }
}
