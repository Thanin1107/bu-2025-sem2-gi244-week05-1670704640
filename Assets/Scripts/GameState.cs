using UnityEngine;

public class GameState : MonoBehaviour
{
    // Step 4.1: add hit count
    private int hitCount = 0;

    // Step 4.1: implement OnTriggerEnter to make game over when hit count is 5
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Animal"))
        {
            hitCount++;
            if (hitCount >= 5)
            {
                Debug.Log("Game Over!");
                Time.timeScale = 0;
            }
        }
    }
}
