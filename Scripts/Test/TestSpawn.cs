using UnityEngine;

public class TestSpawn : MonoBehaviour
{
    //[SerializeField]
    public GameObject objToSpawn;

    void Start()
    {
        Instantiate(objToSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
