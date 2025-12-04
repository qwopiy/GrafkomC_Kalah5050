using UnityEditor.PackageManager;
using UnityEngine;

public class BulletMov : MonoBehaviour
{
    public float speed = 5f;          // kecepatan konstan
    public float maxDistance = 10f;   // jarak maksimum
    private Vector3 startPos;         // posisi awal bullet
    public Vector2 direction = Vector2.up; 

    void Start()
    {
        startPos = transform.position; // simpan posisi awal saat bullet dibuat
    }

    void Update()
    {

        // Gerakkan bullet ke kanan
        transform.position += speed * Time.deltaTime * new Vector3(direction.x, direction.y);

        // Rotasi bullet sesuai arah gerak
        float zAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float offset = -90f;

        transform.rotation = Quaternion.Euler(0, 0, zAngle + offset);

        // Hitung jarak yang sudah ditempuh
        float distance = Vector3.Distance(startPos, transform.position);

        // Jika jarak sudah melebihi maxDistance → hapus bullet
        if (distance >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    public void Shoot(Vector2 _direction)
    {
        direction = _direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(50); // contoh damage
            }
            Destroy(gameObject); // hancurkan bullet setelah mengenai musuh
        }
    }

}