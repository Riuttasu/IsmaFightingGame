using UnityEngine;

public class GroundLogic : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerActions pa = collision.gameObject.GetComponentInParent<PlayerActions>();
        if (pa != null)
        {
            pa.Ground();
        }
    }
}
