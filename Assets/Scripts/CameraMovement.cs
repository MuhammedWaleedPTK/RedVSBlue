using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float movementSpeed = 5f;  // Speed of camera movement
    public float rotationSpeed = 5f;  // Speed of camera rotation
    public float verticalMovementSpeed = 3f;  // Speed of upward and downward movement

    void Update()
    {
        // Camera movement
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalMovement, 0f, verticalMovement) * movementSpeed * Time.deltaTime;
        transform.Translate(movement, Space.Self);

        // Upward and downward movement
        float upMovement = Input.GetKey(KeyCode.E) ? 1f : 0f;
        float downMovement = Input.GetKey(KeyCode.Q) ? -1f : 0f;

        Vector3 verticalMovementVector = new Vector3(0f, (upMovement + downMovement) * verticalMovementSpeed * Time.deltaTime, 0f);
        transform.Translate(verticalMovementVector, Space.World);

        // Camera rotation
        
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            Vector3 rotation = new Vector3(-mouseY, mouseX, 0f) * rotationSpeed * Time.deltaTime;
            transform.eulerAngles += rotation;
        
    }
}
