using UnityEngine;

public class ProceduralObjectGenerator : MonoBehaviour
{
    [Header("Object Settings")]
    public GameObject[] propsPrefab;
    public int amount = 50;
    public float radius = 25f;

    void Start()
    {
        if (propsPrefab.Length == 0) return;

        for (int i = 0; i < amount; i++)
        {
            int rand = Random.Range(0, propsPrefab.Length);
            Vector2 pos = Random.insideUnitCircle * radius;
            Instantiate(propsPrefab[rand], pos, Quaternion.identity);
        }
    }
}
