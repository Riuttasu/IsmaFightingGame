using UnityEngine;

public class PlayerReactGameOver : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameManager.instance != null) { GameManager.instance.GameOver.AddListener(EndGame); }
    }

    private void EndGame()
    {
        gameObject.SetActive(false);
        GameManager.instance.GameOver.RemoveListener(EndGame); // Stops subscribing to event, damage control
    }
}
