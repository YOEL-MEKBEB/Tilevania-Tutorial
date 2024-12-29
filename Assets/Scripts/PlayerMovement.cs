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
    }

    void OnMove(InputValue value){
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void Run(){
        Vector2 playerVelocity = new Vector2(playerSpeed * moveInput.x, player.velocity.y); // gets only the x velocity of moveInput
        player.velocity =  playerVelocity;
    }
}
