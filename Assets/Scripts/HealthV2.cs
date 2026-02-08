using UnityEngine;

public class HealthV2 : MonoBehaviour
{
    // Step 3.6: add max health and accum damage
    public int maxHealth = 100;
    public int accumDamage = 0;

    public void TakeDamage(int damage)
    {
        accumDamage += damage;
        if (accumDamage >= maxHealth)
        {
            Destroy(gameObject);
        }
    }
}
