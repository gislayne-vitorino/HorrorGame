using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidade de movimento do jogador

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
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, 0.5f))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                Debug.Log("AQUIII");
                return true;
            }
        }
        return false;
    }
}
