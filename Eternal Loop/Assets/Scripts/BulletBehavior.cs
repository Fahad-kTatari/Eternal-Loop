using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float maxHeight = 4.5f; // The top boundary of the screen in world units

    void Update()
    {
        // Check if the bullet has passed the top of the screen
        if (transform.position.y > maxHeight)
        {
            Destroy(gameObject); // Destroy the bullet immediately
        }
    }
}
