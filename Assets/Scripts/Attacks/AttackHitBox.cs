using UnityEngine;
/// <summary>
/// Component for attack hurtboxes, when it collides with a player, deals n damage to them
/// </summary>
public class AttackHitBox : MonoBehaviour
{
    [SerializeField]
    private int damage = 0; // Damage the attack will deal to player
    [SerializeField]
    private float KnockBack = 0f; // Knockback the attack will deal to player
    private GameObject _playerHit;
    private float _knockbackLeft = 0f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerLifeSystem pls = collision.gameObject.GetComponent<PlayerLifeSystem>();
        if (pls != null) // Has collided with a player (with layer matrix, shouldnt hit self)
        {
            pls.Hurt(damage); // Hurts the hit player for damage
            _playerHit = pls.gameObject.transform.parent.gameObject; // The player hit
            _knockbackLeft = KnockBack; // Resets knockback
        }
    }
    private void Update()
    {
        // If knockback is meant to be applied
        if (_playerHit != null)
        {
            float knockback = 1 - Mathf.Pow(1 - _knockbackLeft / KnockBack, 4);
        }
    }
}
