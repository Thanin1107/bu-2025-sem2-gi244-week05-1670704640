using UnityEngine;

public class Food : MonoBehaviour
{
    public int attackPoint = 5;

    // Step 3.4: implement OnTriggerEnter to make food deal damage to enemy
    void OnTriggerEnter(Collider other)
    {
        var health = other.GetComponent<HealthV1>();
        if (health != null)
        {
            health.TakeDamage(attackPoint);
        }
        Destroy(gameObject);
    }

    // Step 3.5: refractor and using TryGetComponent to reduce memory allocation
    void OnTriggerEnter2(Collider other)
    {
        if (other.TryGetComponent(out HealthV1 health))
        {
            health.TakeDamage(attackPoint);
        }
        Destroy(gameObject);
    }
}
