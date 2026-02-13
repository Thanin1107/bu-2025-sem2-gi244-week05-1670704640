using UnityEngine;

public class Food : MonoBehaviour
{
    public int attackPoint = 5;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit " + other.gameObject.name);
        var health = other.gameObject.GetComponent<HealthV1>();
        health.TakeDamage(attackPoint);
    }
}
