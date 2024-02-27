
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
        transform.Translate(movement);

        if (IsCollidingWithWall())
        {
            Debug.Log("Sim, confirmo");
            Vector3 movementDirectS = new Vector3(horizontalInput, 0f, -1f).normalized;
            Vector3 movementS = movementDirectS * moveSpeed * Time.deltaTime;

            transform.Translate(movementS);
        }
    }

    private bool IsCollidingWithWall()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 0.5f))
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
