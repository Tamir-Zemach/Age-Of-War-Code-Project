using Unity.Cinemachine;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private CinemachineSplineDolly dollyCart;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float edgeThreshold = 50f;
    [SerializeField] private float pathLength = 1f; // Set to 1 for normalized mode; adjust if using distance-based position
    
    private void Update()
    {
        EdgeScrollWithMouse();
        EdgeScrollWithKeyboard();
    }


    private void EdgeScrollWithMouse()
    {
        float mouseX = Input.mousePosition.x;
        float newPosition = dollyCart.CameraPosition;

        if (mouseX < edgeThreshold)
        {
            newPosition -= moveSpeed * Time.deltaTime;
        }
        else if (mouseX > Screen.width - edgeThreshold)
        {
            newPosition += moveSpeed * Time.deltaTime;
        }

        dollyCart.CameraPosition = Mathf.Clamp(newPosition, 0f, pathLength);
    }

    private void EdgeScrollWithKeyboard()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (Mathf.Abs(horizontalInput) > 0.01f)
        {
            float newPosition = dollyCart.CameraPosition + horizontalInput * moveSpeed * Time.deltaTime;
            dollyCart.CameraPosition = Mathf.Clamp(newPosition, 0f, pathLength);
        }
    }
}
