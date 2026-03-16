using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int _playerNum = 1;
    [Header("Actions")]
    [SerializeField] private List<PlayerInputActions> _playerInputs = new List<PlayerInputActions>();
    [Header("Player Components")]
    [SerializeField] private Animator _animator;
    [Header("Movement Parametres")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpHeight = 3f;
    private InputAction _moveAction, _jumpAction;
    private bool _isGrounded = true;
    private bool _isFalling = false;
    private float _velocity = 0f;
    private float _feetOffset = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (PlayerInputActions action in _playerInputs)
        {
            action.AssignedAction = InputSystem.actions.FindAction(action.Name + _playerNum);
            if (action.AssignedAction == null) { Debug.LogWarning("No action found with the name: " + action.Name + _playerNum); }
            else
            {
                if (action.Name == "Move") { _moveAction = action.AssignedAction; }
                else if (action.Name == "Jump") { _jumpAction = action.AssignedAction; }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Walking
        if (_moveAction.IsPressed())
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") ||
                _animator.GetCurrentAnimatorStateInfo(0).IsName("Walking"))
                Move(_moveAction.ReadValue<Vector2>());
        }
        else
        {
            _animator.SetTrigger("EndWalk");
        }
        // Jumping
        if (_jumpAction.WasPressedThisFrame())
        {
            if ((_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") ||
                _animator.GetCurrentAnimatorStateInfo(0).IsName("Walking")) &&
                _isGrounded)
            {
                Jump();
            }
        }
        // Physics
        if (!_isGrounded)
        {
            transform.position += new Vector3(0, _velocity * Time.deltaTime, 0);
            _velocity += Physics2D.gravity.y * Time.deltaTime;
        }
    }
    // ---- Actions-movement ----
    #region Movement Actions
    /// <summary>
    /// Reads the movement actions of the player and moves if theres no collider in the way
    /// </summary>
    public void Move(Vector2 dir, bool IsKnockBack = false)
    {
        Vector3 origin = new(transform.position.x, transform.position.y-_feetOffset, 0);
        if (!Physics2D.Raycast(origin, dir * Vector2.right, dir.magnitude))
        {
            if (!IsKnockBack)
            {
                _animator.SetTrigger("WalkTrigger");
                transform.position += new Vector3(dir.x * _speed * Time.deltaTime, 0, 0);
            }
            else
            {
                transform.position += new Vector3(dir.x * Time.deltaTime, 0, 0);
            }
        }
    }
    /// <summary>
    /// Impulses the player vertically depending on jumpheight
    /// Ungrounds them, preventing from jumping again
    /// </summary>
    private void Jump()
    {
        _velocity += _jumpHeight;
        _isGrounded = false;
    }
    #endregion
    #region Public methods
    /// <summary>
    /// Stops the player's vertical mobility and grounds them (allows them to jump)
    /// </summary>
    public void Ground()
    {
        _isGrounded = true;
        if (_isFalling) { _animator.SetTrigger("CrushTrigger"); _isFalling = false; }
        _velocity = 0;
    }
    /// <summary>
    /// Gets the current player's grounded state
    /// </summary>
    /// <returns>If the player is on the ground or not</returns>
    public bool GetGroundedState()
    {
        return _isGrounded;
    }
    public void SetFallingState(bool set)
    {
        _isFalling = set;
    }
    #endregion
}
