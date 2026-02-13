using UnityEngine;

public class GameState : MonoBehaviour
{
    public int hitCount = 0;

    void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.name.StartsWith("Dog"))
        //{
        //    hitCount++;
        //}

        //if (other.gameObject.tag == "Enemy")
        if (other.gameObject.CompareTag("Enemy"))
        { 
            hitCount++;
        }

        if (hitCount >= 10)
        {
            Debug.Log("GAME OVER");
            Time.timeScale = 0;
            //Application.Quit();
        }
    }
}
