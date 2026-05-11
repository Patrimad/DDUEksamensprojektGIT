using UnityEngine;
using UnityEngine.InputSystem;

public class HamptRotate : MonoBehaviour
{
    public float rotationSpeed = 10f;

    public PlayerMovement playerMovement;


    void Update()
    {
        Vector2 moveInput = playerMovement.moveInput;

        if (moveInput.sqrMagnitude < 0.01f) return;

        Vector3 targetLocalDirection = new Vector3(moveInput.x, 0, moveInput.y);
        Quaternion targetRotation = Quaternion.LookRotation(targetLocalDirection);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}