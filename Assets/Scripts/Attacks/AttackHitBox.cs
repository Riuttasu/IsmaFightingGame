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
    private float _distance = 0f;
    private float _snap = 2f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerLifeSystem pls = collision.gameObject.GetComponent<PlayerLifeSystem>();
        if (pls != null) // Has collided with a player (with layer matrix, shouldnt hit self)
        {
            PlayerActions pa = collision.gameObject.GetComponentInParent<PlayerActions>();
            if (!pa.GetBlockingState())
            {
                // Damage
                pls.Hurt(damage); 
                // Knockback
                _playerHit = pls.gameObject.transform.parent.gameObject; // The player hit
                Vector2 dir = -_playerHit.transform.right; // Direction to push the player
                RaycastHit2D hit = Physics2D.Raycast(_playerHit.transform.position, dir, KnockBack);
                if (hit.collider == null)
                {
                    _distance = KnockBack; // If theres no wall apply full knockback
                }
                else
                {
                    _distance = hit.distance - 0.2f; // If theres a wall, aplly partial knockback equal to distance
                }
                _knockbackLeft = _distance; // Knockback left is whatever was calculated
            }
        }
    }
    private void Update()
    {
        // If knockback is meant to be applied
        if (_playerHit != null)
        {
            // Removes player reference if knockback is small enough
            if (_knockbackLeft <= 0.01f)
            {
                _playerHit = null;
            }
            else
            {
                // Math stuff
                float t = _knockbackLeft / _distance;
                // Calculates knockback to inflict this frame
                float FrameKnockBack = KnockBack * _snap * Mathf.Sqrt(t);
                // Caps knockback per frame if too high
                FrameKnockBack = Mathf.Min(FrameKnockBack, _knockbackLeft/Time.deltaTime);
                // Gets player movement script to move player back
                PlayerMovement pm = _playerHit.GetComponentInParent<PlayerMovement>();
                if (pm != null)
                {
                    // True indicates that it's knockback
                    pm.Move(new Vector2(FrameKnockBack, 0) * -_playerHit.transform.right, true);
                }
                // Reduces knockback left
                _knockbackLeft -= FrameKnockBack * Time.deltaTime;
            }
        }
    }
}
