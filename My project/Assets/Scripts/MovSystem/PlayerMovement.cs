
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidade de movimento do jogador
    private int wallsLayer;

    void Awake(){
      GameManager.OnGameStateChange += onStateChange;
    }

    void Start (){
      int scenarioLayer = 1 << 7;
      int interactableScenarioLayer = 1 << 8;
      wallsLayer = scenarioLayer | interactableScenarioLayer;
    }

    void onStateChange(GameState gameState){
      if(gameState != GameState.Playing ){
        enabled = false;
      } else{
        enabled = true;
      }
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        Vector3 movement = movementDirection * moveSpeed * Time.deltaTime;

        if (IsCollidingWithScenario(transform.forward) && verticalInput > 0f)
        {
            movement.z = 0f;
        }
        if (IsCollidingWithScenario(-transform.forward) && verticalInput < 0f)
        {
            movement.z = 0f;
        }
        if (IsCollidingWithScenario(transform.right) && horizontalInput > 0f)
        {
            movement.x = 0f;
        }
        if (IsCollidingWithScenario(-transform.right) && horizontalInput < 0f)
        {
            movement.x = 0f;
        }

        transform.Translate(movement);
    }

    private bool IsCollidingWithScenario(Vector3 movementDirection)
    {
       
        RaycastHit hit;
        if (Physics.Raycast(transform.position, movementDirection, out hit, 0.5f, wallsLayer))
          return true;
        return false;
    }

    public void OnDrawGizmos(){
      Gizmos.color = Color.red;
      //Gizmos.DrawRay(transform.position, transform.forward *0.5f);
    }
}