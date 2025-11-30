using UnityEditor.PackageManager;
using UnityEngine;

public class BulletMov : MonoBehaviour
{
    Vector2 moveDir;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject summonsPrefab;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        moveDir.x += 0.01f;
        transform.position += new Vector3(moveDir.x * Time.deltaTime, 0f, 0f);
    }
}