
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

        Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        Vector3 movement = movementDirection * moveSpeed * Time.deltaTime;

        // Verifica se houve colisão em alguma direção e impede o movimento nessa direção
        if (IsCollidingWithWall(transform.forward) && verticalInput > 0f)
        {
            movement.z = 0f;
        }
        if (IsCollidingWithWall(-transform.forward) && verticalInput < 0f)
        {
            movement.z = 0f;
        }
        if (IsCollidingWithWall(transform.right) && horizontalInput > 0f)
        {
            movement.x = 0f;
        }
        if (IsCollidingWithWall(-transform.right) && horizontalInput < 0f)
        {
            movement.x = 0f;
        }

        transform.Translate(movement);
    }

    private bool IsCollidingWithWall(Vector3 direction)
    private bool IsCollidingWithScenario(Vector3 movementDirection)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, 0.5f))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                Debug.Log("AQUIII");
                return true;
            }
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
