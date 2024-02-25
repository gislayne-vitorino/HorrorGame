using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidade de movimento do jogador

    void Update()
    {
        // Obtém os inputs de movimento horizontal e vertical
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calcula a direção de movimento com base nos inputs
        Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        Vector3 movement = movementDirection * moveSpeed * Time.deltaTime;

        transform.Translate(movement);
    }
}