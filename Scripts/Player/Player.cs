using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    PlayerInput pInput;
    Collider2D col;

    // Player Stats
    public int health = 100;
    public int damage = 100;
    float iFrameWindow = 0.2f;
    bool inIFrame = false;

    // Untuk Translasi
    [SerializeField]
    float movSpeed = 5f;
    Vector2 moveDir;
    Vector2 moveDirNormalized;

    // Untuk Rotasi
    readonly float offset = -90f;
    float zAngle;
    Vector2 lastNonZeroDir = Vector2.right;

    // Untuk Shooting
    public GameObject bullet;

    private void Awake()
    {
        pInput = new PlayerInput();
        col = GetComponent<Collider2D>();
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

    // Translasi
    // P' = P + T
    void Move()
    {
        NormalizeDirection(moveDir.x, moveDir.y);

        if (moveDirNormalized.sqrMagnitude > 0.001f)
        {
            lastNonZeroDir = moveDirNormalized;
        }

        Vector3 translation = Vector3.zero;
        translation.x = moveDirNormalized.x * movSpeed * Time.deltaTime;
        translation.y = moveDirNormalized.y * movSpeed * Time.deltaTime;
        transform.position += translation;
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

    public void ShootBullet(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            bullet.GetComponent<BulletMov>().Shoot(lastNonZeroDir);
            Instantiate(bullet, transform.position, Quaternion.identity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GetHit(10);
            Debug.Log("Player Health: " + health);
        }
    }

    private IEnumerator IFrame()
    {
        if (!inIFrame)
        {
            inIFrame = true;
            yield return new WaitForSeconds(iFrameWindow);
            inIFrame = false;
        }
    }

    private void GetHit(int damage)
    {
        if (!inIFrame)
        {
            health -= damage;
            Debug.Log("Player Health: " + health);
            StartCoroutine(IFrame());
        }
    }
}