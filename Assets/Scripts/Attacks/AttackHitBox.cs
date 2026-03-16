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
            // Removes player reference if knockback is gone
            if (_knockbackLeft <= 0f)
            {
                _playerHit = null;
            }
            else
            {
                float t = 1 - _knockbackLeft / KnockBack;
                float multi = 1 - (t*t);
                // Calculates knockback to inflict
                float knockback = KnockBack * multi;
                // Gets player movement script to move player back
                PlayerMovement pm = _playerHit.GetComponentInParent<PlayerMovement>();
                if (pm != null)
                {
                    pm.Move(new Vector2(knockback, 0) * -_playerHit.transform.right,true);
                }
                // Reduces knockback left
                _knockbackLeft -= knockback*Time.deltaTime;
            }
        }
    }
}
