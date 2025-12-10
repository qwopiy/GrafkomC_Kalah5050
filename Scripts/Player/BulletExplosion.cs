using UnityEngine;

public class BulletExplosion : MonoBehaviour
{
    public float explosionScale = 3f;
    public float explosionSpeed = 1f;
    float currentScale;

    void Start()
    {
        currentScale = transform.localScale.x;
        explosionScale *= currentScale;
    }

    void Update()
    {
        Debug.Log(currentScale);
        currentScale += explosionSpeed * Time.deltaTime;
        UpdateScale();
        if (currentScale >= explosionScale)
        {
            Destroy(gameObject);
        }
    }

    // Rumus Skalasi
    // P' = P * S
    // (x',y',z') = (x * S, y * S, z * S)
    void UpdateScale()
    {
        transform.localScale = Vector3.one * currentScale;
    }
}
