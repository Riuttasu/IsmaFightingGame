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
}
