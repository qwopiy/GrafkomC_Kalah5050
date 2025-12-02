using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bullet;

    public void ShootBullet(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            //bullet.GetComponent<BulletMov>().Shoot(Vector2.right);
        }
    }
}
