using Unity.VisualScripting;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public int hitCount = 0;
    void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag == "Enemy")
        //{
        //    hitCount++;
        //}
        if (other.gameObject.CompareTag("Enemy"))
        {
            hitCount++;
        }
        if (hitCount >= 500)
        {
            Debug.Log("Game over");
            Time.timeScale = 0f;
        }
    }
    
}
