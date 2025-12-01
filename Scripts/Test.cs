using UnityEditor.PackageManager;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float speed = 5f;          // kecepatan konstan
    public float maxDistance = 10f;   // jarak maksimum
    private Vector3 startPos;         // posisi awal bullet

    void Start()
    {
        startPos = transform.position; // simpan posisi awal saat bullet dibuat
    }

    void Update()
    {
        Shoot(Vector3.up);
    }

    void Shoot(Vector3 direction)
    {
        // Gerakkan bullet ke kanan
        transform.position += direction * speed * Time.deltaTime;

        // Hitung jarak yang sudah ditempuh
        float distance = Vector3.Distance(startPos, transform.position);

        // Jika jarak sudah melebihi maxDistance → hapus bullet
        if (distance >= maxDistance)
        {
            Destroy(gameObject);
        }
    }
}