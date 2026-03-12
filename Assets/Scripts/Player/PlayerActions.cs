using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerActions : MonoBehaviour
{
    // ------ Editor parametres ------
    #region Editor parametres
    [Header("Attack Parametres")]
    [SerializeField] private int _playerNum = 1;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpHeight = 3f;
    [Header("Player Components")]
    [SerializeField] private Animator _animator;
    private bool isDoingAction = false; private bool isblocking = false; private bool isCrushing = false;
    
    private bool grounded = true;
    // Jump physics
    private float velocity = 0f;
    #endregion
    // ------ parametres ------
    #region Parametres
    // Physics
    private float _velocity = 0f;
    // Actions
    private bool _isGrounded, _isBlocking, _isDoingAction;
    // 
    private InputAction _punchAction, _moveAction, _blockAction, _jumpAction, _crushAction;
    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _punchAction = InputSystem.actions.FindAction("Punch"+_playerNum);
        _moveAction = InputSystem.actions.FindAction("Move" + _playerNum);
        _blockAction = InputSystem.actions.FindAction("Block" + _playerNum);
        _jumpAction = InputSystem.actions.FindAction("Jump" + _playerNum);
        _crushAction = InputSystem.actions.FindAction("Crush" + _playerNum);
    }

    // Update is called once per frame
    void Update()
    {
        // Blocking input actions
        // Punch action
        if (_punchAction != null && _punchAction.WasPressedThisFrame() && !isDoingAction && !isblocking)
        {
            
        }
        // Block action
        else if (_blockAction != null && _blockAction.WasPressedThisFrame() && !isDoingAction && !isblocking)
        {
            Block();
        }
        // Move action
        else if (_moveAction != null && _moveAction.IsPressed() && !isDoingAction && !isblocking)
        {
            Move();
        }
        // Non-blocking input actions
        if (_jumpAction.WasPressedThisFrame() && grounded)
        {
            Jump();
        }
        // Physics
        if (!grounded)
        {
            transform.position += new Vector3(0, velocity * Time.deltaTime, 0);
            velocity += Physics2D.gravity.y * Time.deltaTime;
        }
    }
    private void Move()
    {
        Vector2 dir = _moveAction.ReadValue<Vector2>();
        Vector3 origin = new(transform.position.x, transform.position.y, 0);
        if (!Physics2D.Raycast(origin, dir * Vector2.right, dir.magnitude))
        {
            transform.position += new Vector3(dir.x * _speed * Time.deltaTime, 0, 0);
        }
    }
    private void Punch()
    {
        _animator.SetTrigger("PunchTrigger");
    }
    private void Block()
    {

        _isBlocking = false;
    }
    private void Jump()
    {
        velocity += _jumpHeight;
        grounded = false;
    }
    public void Ground()
    {
        grounded = true;
        velocity = 0;
    }
    public int GetPlayerNumber()
    {
        return _playerNum;
    }
    public bool GetBlockingState()
    {
        return _isBlocking;
    }
}
