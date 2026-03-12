using UnityEngine;

public class ChangecolorOnHit : MonoBehaviour
{
    private SpriteRenderer sr;
    private void Awake()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        sr.color = Color.red;
        if (GameManager.instance != null) GameManager.instance.AddHp(-10, 2);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        sr.color = Color.white;
    }
}
