using UnityEngine;
public class Enemy : MonoBehaviour
{
    public GameObject enemyExplosionParticleSystemPrefab;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            GameManager.instance.GameOver();
        }

        if (other.gameObject.CompareTag("Bullet"))
        {
            GameObject enemyExplosion = Instantiate(enemyExplosionParticleSystemPrefab, transform.position, transform.rotation);
            Destroy(enemyExplosion, 0.75f);
            Destroy(gameObject);
            Destroy(other.gameObject);
            GameManager.instance.AddScore();
        }

        if (other.gameObject.CompareTag("EnemyDiffuser"))
        {
            Destroy(gameObject); // Destroy the enemy
            GameManager.instance.LoseLife(); // Player loses a life
        }
    }
}
