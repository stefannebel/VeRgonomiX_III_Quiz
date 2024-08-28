using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 2.0f; // Adjust the sensitivity of mouse movement

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Rotate the object based on mouse input
        transform.Rotate(Vector3.up * mouseX * sensitivity);
        transform.Rotate(Vector3.left * mouseY * sensitivity);

        // Limit the vertical rotation to prevent flipping
        float currentRotationX = transform.eulerAngles.x;
        if (currentRotationX > 180f)
        {
            currentRotationX -= 360f;
        }

        currentRotationX = Mathf.Clamp(currentRotationX, -80f, 80f);

        // Apply the rotation
        transform.rotation = Quaternion.Euler(currentRotationX, transform.eulerAngles.y, 0f);
    }
}
