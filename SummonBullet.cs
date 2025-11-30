using System.Collections;
using UnityEditor.PackageManager;
using UnityEngine;

public class SummonBullet : MonoBehaviour
{
    public GameObject summonsPrefab;
    public float interval = 10f; // jeda antar summon

    void Start()
    {
        StartCoroutine(SummonRoutine());
    }

    IEnumerator SummonRoutine()
    {
        for (int i = 0; i < 10; i++)
        {
            // spawn bullet
            GameObject summonsGO = Instantiate(summonsPrefab, transform.position, transform.rotation);
            summonsGO.transform.SetParent(transform);

            yield return new WaitForSeconds(interval); // jeda sebelum spawn berikutnya
        }
    }
}
