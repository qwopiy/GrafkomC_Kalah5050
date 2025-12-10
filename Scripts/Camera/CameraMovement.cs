using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Transform objToFollow;
    readonly float zOffset = -10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objToFollow = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(objToFollow.position.x, objToFollow.position.y, zOffset);
    }
}
