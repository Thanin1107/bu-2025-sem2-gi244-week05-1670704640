using UnityEngine;

public class HealthV2 : MonoBehaviour
{
    public int maxhp = 100;
    private int accumDamage = 0;
    public void TakeDamage(int damage)
    {
        accumDamage += damage;
        if (accumDamage >= maxhp)
        {
            Destroy(gameObject);
        }
    }
}
