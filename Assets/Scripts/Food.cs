using UnityEngine;

public class Food : MonoBehaviour
{
    public int attackPoint = 5;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit"+ other.gameObject.name);
        //var health = other.gameObject.GetComponent<HealthV1>();
        //if(health != null )
        //{
        // health.TakeDamage(attackPoint);
        //}
        if (other.gameObject.TryGetComponent<HealthV1>(out HealthV1 health))
        {
            health.TakeDamage(attackPoint);
        }
        Destroy(gameObject);

    }
}
