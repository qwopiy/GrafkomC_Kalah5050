using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject[] enemyPrefabs; // tinggal drag & drop prefab musuh
    public float spawnInterval = 3f;
    public float difficultyRate = 0.97f; // spawn semakin cepat

    [Header("Spawn Area")]
    public float spawnRadius = 15f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
            spawnInterval *= difficultyRate;
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0) return;

        // pilih musuh secara random dari array
        int randIndex = Random.Range(0, enemyPrefabs.Length);

        // posisi spawn acak di luar pemain dengan radius melingkar
        Vector2 randomPos = Random.insideUnitCircle.normalized * spawnRadius;
        Vector3 spawnPos = new Vector3(randomPos.x, randomPos.y, 0);

        Instantiate(enemyPrefabs[randIndex], spawnPos, Quaternion.identity);
    }
}
