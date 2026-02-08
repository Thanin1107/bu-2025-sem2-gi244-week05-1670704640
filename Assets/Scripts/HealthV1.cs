using UnityEngine;

public class HealthV1 : MonoBehaviour
{
    // Step 3.1: add health variable
    public int health = 100;

    public void TakeDamage(int damage)
    {
        // Step 3.2: reduce health and destroy game object if health is less than or equal to 0
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
