using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CameraMovement : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 0f;
    public float runSpeed = 0f;
    public float jumpPower = 0f;
    public float gravity = 0f;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 3f;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;

    private bool canMove = true;

    void Awake(){
      GameManager.OnGameStateChange += onStateChange;
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        characterController.detectCollisions = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void onStateChange(GameState gameState){
      if(gameState != GameState.Playing ){
        enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
      } else{
        enabled = true;
      }
    }

    void Update()
    {
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}