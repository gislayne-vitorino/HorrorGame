
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
        Vector3 movement = movementDirection * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    private bool IsCollidingWithScenario(Vector3 movementDirection)
    {
      Vector3 raycastOrigin = new Vector3(transform.position.x, 0, transform.position.z);
      Vector3 raycastDirection = raycastOrigin;
      RaycastHit hit;

      if (movementDirection.z > 0)
        raycastDirection = transform.forward;
      else if (movementDirection.z < 0)
        raycastDirection = Quaternion.AngleAxis(180, Vector3.up) * transform.forward;
      else if (movementDirection.x > 0)
        raycastDirection = transform.right;
      else if (movementDirection.x < 0)
        raycastDirection = Quaternion.AngleAxis(180, Vector3.up) * transform.right;
      
      if (Physics.Raycast(raycastOrigin, raycastDirection, out hit, 0.5f, wallsLayer))
      {
          //Debug.Log("AQUIII");
          return true;
      }

        return false;
    }
}

/*
        Debug.DrawLine (transform.position, (transform.forward + movementDirection)*0.5f, Color.magenta);
        
        if (Physics.Raycast(transform.position, transform.position + movementDirection, out hit, 0.5f, wallsLayer))
*/