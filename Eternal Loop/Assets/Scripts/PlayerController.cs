using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player
    public SpriteRenderer playerSprite; // Reference to the SpriteRenderer of the player
    public float moveSpeed = 10f; // Player's movement speed
    private float inputMoveX; // Variable to store input direction

    // Bullet
    public Transform firePoint; // Position where bullets should spawn
    public float bulletSpeed = 10f; // Speed of the bullet
    public GameObject bulletPrefab; // Prefab of the bullet object

    // Start is called before the first frame update
    void Start()
    {
        // Rotate the player sprite by 90 degrees to make it "straight"
        transform.rotation = Quaternion.Euler(0, 0, 90);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.IsGameOver())
        {
            // Get horizontal input
            inputMoveX = Input.GetAxisRaw("Horizontal"); // -1 for Left, +1 for Right

            // Move the player left or right relative to the screen (invert direction to fix controls)
            transform.Translate(Vector3.up * -inputMoveX * moveSpeed * Time.deltaTime);

            // If the Space button is pressed, shoot a bullet
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }
        }
    }

    // This function creates a bullet and fires it straight up the screen
    private void Shoot()
    {
        // Instantiate a new bullet at the firePoint's position and rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Access the Rigidbody2D of the bullet
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();

        // Set the bullet's velocity to move upwards relative to the world
        bulletRigidbody.velocity = Vector2.up * bulletSpeed; // Bullet moves up the screen

        // Automatically destroy the bullet after 5 seconds if it hasn't left the screen
        Destroy(bullet, 5f);
    }

}
