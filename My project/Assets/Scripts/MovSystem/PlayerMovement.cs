
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidade de movimento do jogador
    private int wallsLayer;

    void Start (){
      int scenarioLayer = 1 << 7;
      int interactableScenarioLayer = 1 << 8;
      wallsLayer = scenarioLayer | interactableScenarioLayer;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if(IsCollidingWithScenario(new Vector3(horizontalInput,0f,0f).normalized)){
          horizontalInput = 0f;
        }
        if(IsCollidingWithScenario(new Vector3(0f,0f,verticalInput).normalized)){
          verticalInput = 0f;
        }

        Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (!IsCollidingWithScenario(movementDirection)){
          Vector3 movement = movementDirection * moveSpeed * Time.deltaTime;
          transform.Translate(movement);
        }
    }

    private bool IsCollidingWithScenario(Vector3 movementDirection)
    {
        RaycastHit hit;
        Debug.DrawLine(transform.position, movementDirection*0.5f,Color.magenta);
        Debug.Log(transform.forward);
        if (Physics.Raycast(transform.position, movementDirection, out hit, 0.5f, wallsLayer))
        {
            Debug.Log("AQUIII");
            return true;
        }

        return false;
    }

    public void OnDrawGizmos(){
      Gizmos.color = Color.red;
      //Gizmos.DrawRay(transform.position, transform.forward *0.5f);
    }
}
