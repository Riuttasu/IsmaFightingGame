using UnityEngine;
/// <summary>
/// Component for attack hurtboxes, when it collides with a player, deals n damage to them
/// </summary>
public class AttackHitBox : MonoBehaviour
{
    [SerializeField]
    private int damage = 0; // Damage the attack will deal to player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerLifeSystem pls = collision.gameObject.GetComponent<PlayerLifeSystem>();
        if (pls != null ) // Has collided with a player (with layer matrix, shouldnt hit self)
        {
            pls.Hurt(damage); // Hurts the hit player for damage
        }
    }
}
