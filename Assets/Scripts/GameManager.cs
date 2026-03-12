using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Singleton which manages UI, player HP
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int hp1,hp2;
    [Header("Parametres")]
    [SerializeField] private int maxhp = 100;
    [Header("UI")] 
    [SerializeField]
    private TMPro.TextMeshProUGUI hp1_text;
    [SerializeField]
    private TMPro.TextMeshProUGUI hp2_text;
    [SerializeField]
    private GameObject player1win;
    [SerializeField] 
    private GameObject player2win;
    public UnityEvent GameOver;
    private void Awake()
    {
        // Checks for other managers, deletes self if theres already one
        if (instance != null)
        {
            Debug.LogWarning("Another instance of GameManager present");
            Destroy(this);
        }
        // Initial state of the game otherwise
        else
        {
            player1win.SetActive(false);
            player2win.SetActive(false);
            instance = this;
            hp1 = hp2 = maxhp;
            UpdateGUI();
        }
    }
    /// <summary>
    /// Lets other components get a player's HP
    /// </summary>
    /// <param name="playernum"></param>
    /// <returns></returns>
    public int GetHP(int playernum)
    {
        if (playernum == 1) return hp1;
        else return hp2;
    }
    /// <summary>
    /// Updates hp UI
    /// </summary>
    private void UpdateGUI()
    {
        if (hp1_text != null)
        {
            hp1_text.text = $"Player 1 hp: {hp1}";
        }
        if (hp2_text != null)
        {
            hp2_text.text = $"Player 2 hp: {hp2}";
        }
    }
    /// <summary>
    /// Changes the hp of a certain player
    /// </summary>
    /// <param name="n"></param>
    /// <param name="player"></param>
    public void AddHp(int n, int player)
    {
        if (player == 1)
        {
            hp1 += n;
            if (hp1<0)
            {
                hp1 = 0; 
            }
        }
        else
        {
            hp2 += n;
            if (hp2 < 0)
            {
                hp2 = 0;
            }
        }
        if (hp1 == 0) { player2win.SetActive(true); GameOver.Invoke(); }
        if (hp2 == 0) { player1win.SetActive(true); GameOver.Invoke(); }
        UpdateGUI();
    }
}
