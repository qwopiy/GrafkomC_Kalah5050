using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    PlayerInput pInput;
    Collider2D col;
    public GameObject gun;
    Animator animator;

    // Player Stats
    public static int health = 100;
    float iFrameWindow = 0.2f;
    bool inIFrame = false;

    // Untuk Translasi
    [SerializeField]
    float movSpeed = 5f;
    Vector2 moveDir;
    Vector2 moveDirNormalized;
    bool isWalking = false;

    // Untuk Rotasi
    float zAngle;
    Vector2 lastNonZeroDir = Vector2.right;
    float offsetAmount = 0.7f; // untuk tembak

    // Untuk Shooting
    public GameObject bullet;

    private void Awake()
    {
        pInput = new PlayerInput();
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
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
        health = 100;
        GameStateManager.InitState();
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
       moveDirNormalized = NormalizeDirection(moveDir.x, moveDir.y);

        if (moveDirNormalized.sqrMagnitude > 0.001f)
        {
            lastNonZeroDir = moveDirNormalized;
        }

        Vector3 translation = Vector3.zero;
        translation.x = moveDirNormalized.x * movSpeed * Time.deltaTime;
        translation.y = moveDirNormalized.y * movSpeed * Time.deltaTime;
        transform.position += translation;

        // Animasi jalan
        if (moveDirNormalized.sqrMagnitude > 0.001f)
        {
            isWalking = true;
            animator.SetBool("isWalking", true);
        }
        else
        {
            isWalking = false;
            animator.SetBool("isWalking", false);
        }

        if (isWalking)
        {
            animator.SetFloat("moveX", moveDirNormalized.x);
            animator.SetFloat("moveY", moveDirNormalized.y);
        }
    }

    void Rotate()
    {
        // Rumus rotasi penting
        zAngle = Mathf.Atan2(lastNonZeroDir.y, lastNonZeroDir.x) * Mathf.Rad2Deg;

        gun.transform.rotation = Quaternion.Euler(0, 0, zAngle);

        // Offset gun
        Vector2 offset = (offsetAmount * lastNonZeroDir);
        gun.transform.position = new Vector2(transform.position.x + offset.x, transform.position.y + offset.y);

        // flip gun kalo hadap kiri
        if (lastNonZeroDir.x < 0)
        {
            gun.GetComponent<SpriteRenderer>().flipY = true;
        }
        else
        {
            gun.GetComponent<SpriteRenderer>().flipY = false;
        }
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

    // Rumus normalisasi
    Vector2 NormalizeDirection(float x, float y)
    {
        Vector2 normalDir;
        if (x != 0 && y != 0)
        {
            normalDir.x = x * Mathf.Sqrt(2) / 2;
            normalDir.y = y * Mathf.Sqrt(2) / 2;
        }
        else
        {
            normalDir.x = x;
            normalDir.y = y;
        }
        return normalDir;
    }

    Vector2 NormalizeDirection(Vector2 dir)
    {
        return NormalizeDirection(dir.x, dir.y);
    }

    public void ShootBullet(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // offset supaya bullet muncul di depan gun
            Vector2 bulletStartPos = new Vector2(transform.position.x + ((offsetAmount + 1.1f) * lastNonZeroDir.x), transform.position.y + ((offsetAmount + 1.2f) * lastNonZeroDir.y));
            bullet.GetComponent<Bullet>().Shoot(lastNonZeroDir);
            Instantiate(bullet, bulletStartPos, Quaternion.identity);
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
            if (health <= 0)
            {
                Debug.Log("Player Died");
                //Destroy(gameObject);
                GameStateManager.GoToMenu();
            }
        }
    }
}