using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTranform;
    [SerializeField] private float speed = 20f;
    [SerializeField] private float screenBorderThickness = 10f;
    [SerializeField] Vector2 screenXLimits;
    [SerializeField] Vector2 screenZLimits;

    [SerializeField] private float offset = 50f;

    private Controls controls;
    private Vector2 previousInput;

    public void Start()
    {
        playerCameraTranform.gameObject.SetActive(true);
        controls = new Controls();

        controls.Player.MoveCamera.performed += SetPreviousInput;
        controls.Player.MoveCamera.canceled += SetPreviousInput;

        controls.Enable();
    }

    private void Update()
    {
        UpdateCameraPosition();
    }

    public void SetCameraToFocusOnAObject(Transform objectToGo)
    {
        playerCameraTranform.transform.position = new Vector3(objectToGo.position.x,
            playerCameraTranform.position.y, objectToGo.position.z - offset);
    }

    private void UpdateCameraPosition()
    {
        Vector3 pos = playerCameraTranform.position;

        if (previousInput == Vector2.zero)
        {
            Vector3 cursorMovement = Vector3.zero;

            Vector2 cursorPosition = Mouse.current.position.ReadValue();

            if (cursorPosition.y >= Screen.height - screenBorderThickness)
            {
                cursorMovement.z += 1;
            }
            else if (cursorPosition.y <= screenBorderThickness)
            {
                cursorMovement.z -= 1;
            }

            if (cursorPosition.x >= Screen.width - screenBorderThickness)
            {
                cursorMovement.x += 1;
            }
            else if (cursorPosition.x <= screenBorderThickness)
            {
                cursorMovement.x -= 1;
            }

            pos += cursorMovement.normalized * speed * Time.deltaTime;
        }
        else
        {
            pos += new Vector3(previousInput.x, 0, previousInput.y) * speed * Time.deltaTime;
        }

        pos.x = Mathf.Clamp(pos.x, screenXLimits.x, screenXLimits.y);
        pos.z = Mathf.Clamp(pos.z, screenZLimits.x, screenZLimits.y);

        playerCameraTranform.transform.position = pos;
    }

    private void SetPreviousInput(InputAction.CallbackContext ctx)
    {
        previousInput = ctx.ReadValue<Vector2>();
    }
}
