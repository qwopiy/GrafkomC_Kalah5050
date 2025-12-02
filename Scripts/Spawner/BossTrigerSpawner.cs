using UnityEngine;

public class BossTriggerSpawner : MonoBehaviour
{
    public GameObject bossPrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Instantiate(bossPrefab, Vector3.zero, Quaternion.identity);
        }
    }
}
