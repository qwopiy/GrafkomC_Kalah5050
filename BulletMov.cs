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
        transform.position += new Vector3(direction.x, direction.y) * speed * Time.deltaTime;

        // Hitung jarak yang sudah ditempuh
        float distance = Vector3.Distance(startPos, transform.position);

        // Jika jarak sudah melebihi maxDistance → hapus bullet
        if (distance >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    void Shoot(Vector2 _direction)
    {
        direction = _direction;
    }
}