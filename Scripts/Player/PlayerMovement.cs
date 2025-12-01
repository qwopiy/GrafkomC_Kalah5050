using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput pInput;

    // Untuk Translasi
    [SerializeField]
    float movSpeed = 5f;
    Vector2 moveDir;
    Vector2 moveDirNormalized;

    // Untuk Rotasi
    [SerializeField]
    float rotationSpeed = 100f;
    readonly float offset = -90f;
    float zAngle;
    Vector2 lastNonZeroDir = Vector2.right;

    private void Awake()
    {
        pInput = new PlayerInput();
    }

    private void OnEnable()
    {
        pInput.Enable();
    }

    private void OnDisable()
    {
        pInput.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        NormalizeDirection(moveDir.x, moveDir.y);

        if (moveDirNormalized.sqrMagnitude > 0.001f)
        {
            lastNonZeroDir = moveDirNormalized;
        }

        Vector3 newPosition = transform.position;
        newPosition.x += moveDirNormalized.x * movSpeed * Time.deltaTime;
        newPosition.y += moveDirNormalized.y * movSpeed * Time.deltaTime;
        transform.position = newPosition;
    }

    void Rotate()
    {
        // Rumus rotasi penting
        zAngle = Mathf.Atan2(lastNonZeroDir.y, lastNonZeroDir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, zAngle + offset);
    }

    public void CalculateUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moveDir.y += 1;
        }
        if (context.canceled)
        {
            moveDir.y -= 1;
        }
    }

    public void CalculateDown(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moveDir.y -= 1;
        }
        if (context.canceled)
        {
            moveDir.y += 1;
        }
    }

    public void CalculateLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moveDir.x -= 1;
        }
        if (context.canceled)
        {
            moveDir.x += 1;
        }
    }

    public void CalculateRight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moveDir.x += 1;
        }
        if (context.canceled)
        {
            moveDir.x -= 1;
        }
    }

    // Rumus translasi penting
    void NormalizeDirection(float x, float y)
    {
        if (x != 0 && y != 0)
        {
            moveDirNormalized.x = x * Mathf.Sqrt(2) / 2;
            moveDirNormalized.y = y * Mathf.Sqrt(2) / 2;
        }
        else
        {
            moveDirNormalized.x = x;
            moveDirNormalized.y = y;
        }
    }
}
