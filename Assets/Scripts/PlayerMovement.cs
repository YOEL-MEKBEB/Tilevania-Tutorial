using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
   
    Vector2 moveInput;
    Vector2 upInput;
    [SerializeField] float playerSpeed = 2f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float climbSpeed = 2f;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;

    //[SerializeField] Tilemap climbMap;

    Rigidbody2D player;
    CapsuleCollider2D collider2D;
    BoxCollider2D boxCollider2D;
    Animator animator;

    float gravityAtStart;

    int layerMask;

    bool playerHasHorizontalSpeed; // bool to check if movement is happening
    bool isAlive = true;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider2D = GetComponent<CapsuleCollider2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        gravityAtStart = player.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(isAlive){
            Run();
            FlipSprite();
            ClimbLadder();
            Die();
        }
       
    }

    void OnMove(InputValue value){
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value){

        if(isAlive){
            layerMask = LayerMask.GetMask("Ground");

            if(value.isPressed && boxCollider2D.IsTouchingLayers(layerMask)){
                player.velocity += new Vector2(0f, jumpSpeed);
            }
        }

    }

    void OnFire(InputValue value){
        if(!isAlive){return;}
        Instantiate(bullet, gun.position, transform.rotation);
    }


    void Run(){
        Vector2 playerVelocity = new Vector2(playerSpeed * moveInput.x, player.velocity.y); // gets only the x velocity of moveInput
        player.velocity =  playerVelocity;

        playerHasHorizontalSpeed = Mathf.Abs(player.velocity.x) > Mathf.Epsilon;
        // if(playerHasHorizontalSpeed){
        //     animator.SetBool("isRunning", true);
        // }
        // else{
        //     animator.SetBool("isRunning", false);
        // }
        animator.SetBool("isRunning", playerHasHorizontalSpeed); // same effect as the commented code above
    

    }
    
    void FlipSprite(){
        // Mathf.Sign returns 1 if positive, -1 if negative
        // Mathf.Epsilon is a really small number close to zero but not zero;
        playerHasHorizontalSpeed = Mathf.Abs(player.velocity.x) > Mathf.Epsilon;

        if(playerHasHorizontalSpeed){
            transform.localScale = new Vector2(Mathf.Sign(player.velocity.x), 1f);
        }
        
    }


    void ClimbLadder(){

        layerMask = LayerMask.GetMask("Ladder");

        if(!collider2D.IsTouchingLayers(layerMask)){
            player.gravityScale = gravityAtStart;
            animator.SetBool("isClimbing", false);
            return;
        }

        player.gravityScale = 0f;
        Vector2 climbVelocity = new Vector2(player.velocity.x, climbSpeed * moveInput.y);
        player.velocity =  climbVelocity;

        bool playerHasVerticalSpeed = Mathf.Abs(player.velocity.y) > Mathf.Epsilon;
        animator.SetBool("isClimbing", playerHasVerticalSpeed);
    }

   
    void Die(){
        layerMask = LayerMask.GetMask("Enemy", "Hazards");
        
        if(collider2D.IsTouchingLayers(layerMask) || boxCollider2D.IsTouchingLayers(layerMask)){
            Debug.Log("alive is false");
            isAlive = false;
            player.velocity += new Vector2(0f, jumpSpeed);
            animator.SetBool("isRunning", false);
            animator.SetTrigger("Dying");
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
            
        }
    }
   
}
