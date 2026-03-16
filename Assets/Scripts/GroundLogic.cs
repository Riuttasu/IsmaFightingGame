using UnityEngine;

public class GroundLogic : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMovement pm = collision.gameObject.GetComponentInParent<PlayerMovement>();
        if (pm != null)
        {
            pm.Ground();
        }
    }
}
