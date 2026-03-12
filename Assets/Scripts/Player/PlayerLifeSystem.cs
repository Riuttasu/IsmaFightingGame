using UnityEngine;

public class PlayerLifeSystem : MonoBehaviour
{
    private PlayerActions pa;
    private int playerNum;
    private void Start()
    {
        pa = gameObject.GetComponentInParent<PlayerActions>();
        if (pa != null )
        {
            playerNum = pa.GetPlayerNumber();
        }
        else
        {
            Debug.LogError("No PlayerController attached to gameObject");
            Destroy(this);
        }
        // Subscribes to the game over event if theres a GameManager 
        if (GameManager.instance != null)
        {
            GameManager.instance.GameOver.AddListener(EndGame);
        }
    }
    /// <summary>
    /// Removes n hitpoints from the player if theres a GameManager present, 
    /// has the capacity to change its sprite color too
    /// </summary>
    public void Hurt(int n)
    {
        if (GameManager.instance != null)
        {
            // Not blocking, take n damage
            if (!pa.GetBlockingState())
            {
                GameManager.instance.AddHp(-n, playerNum);
            }
            
        }
    }
    // Dissapears when game is over, unsubscribes from event
    private void EndGame()
    {
        gameObject.SetActive(false);
        GameManager.instance.GameOver.RemoveListener(EndGame); // Stops subscribing to event, damage control
    }
}
