using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerShoot : MonoBehaviour
{
    public InputSystem_Actions input;
    public Camera cam;
    public Transform firePoint; 
    public GameObject bulletPrefab;
    public float bulletSpeed = 20f;

    public float fireRate = 0.3f;
    private float nextShootTime = 0f;


    private void Start()
    {
        input = new InputSystem_Actions();
        input.Enable();
    }
    void Update()
    {
        if (input.Player.Shoot.IsPressed() && Time.time >= nextShootTime)
        {
            Shoot();
            nextShootTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();

        Ray ray = cam.ScreenPointToRay(mousePos);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        float enter;
        if (groundPlane.Raycast(ray, out enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);

            Vector3 dir = hitPoint - firePoint.position;
            dir.y = 0f;
            dir.Normalize();

            GameObject bullet = Instantiate(
                bulletPrefab,
                firePoint.position,
                bulletPrefab.transform.rotation
            );

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.linearVelocity = dir * bulletSpeed;
        }
    }

    private void OnDestroy()
    {
        input.Player.Disable();
        input.UI.Disable();
    }
}
