using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab; // Drag your arrow prefab here in the Unity editor
    public float projectileSpeed = 10f;
    public float fireRate = 1f; // Adjust the fire rate as needed
    private float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime) // 0 represents the left mouse button
        {
            ShootProjectile();
            nextFireTime = Time.time + 1f / fireRate; // Calculate the next allowed firing time
        }
    }

    void ShootProjectile()
    {
        // Get the mouse position in world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction from the player to the mouse position
        Vector2 direction = (mousePosition - transform.position).normalized;

        // Calculate the rotation angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Instantiate the projectile at the player's position with the calculated rotation
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));

        // Set the velocity of the projectile with constant speed
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        projectileRb.velocity = direction.normalized * projectileSpeed;

        // You may want to destroy the projectile after a certain time or when it goes off-screen
        Destroy(projectile, 2f); // Adjust the time as needed
    }
}
