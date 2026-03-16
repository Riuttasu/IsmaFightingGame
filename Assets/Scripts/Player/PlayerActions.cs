using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[System.Serializable]
public class PlayerInputActions
{
    [SerializeField] private string _name;
    public string Name
    { get { return _name; } }
    private InputAction _assignedAction;
    public InputAction AssignedAction
    {
        get { return _assignedAction; }
        set { _assignedAction = value; }
    }
}
public class PlayerActions : MonoBehaviour
{
    // ------ Editor parametres ------
    #region Editor parametres
    [Header("Player info")]
    [SerializeField] private int _playerNum = 1;
    [Header("Attack Parametres")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpHeight = 3f;
    [Header("Player Components")]
    [SerializeField] private Animator _animator;
    [Header("Actions")]
    [SerializeField] private List<PlayerInputActions> _playerInputs = new List<PlayerInputActions>();
    private bool isDoingAction = false; private bool _isBlocking = false; private bool isCrushing = false;

    private bool grounded = true;
    // Jump physics
    private float velocity = 0f;
    #endregion
    // ------ parametres ------
    #region Parametres
    // Physics
    private float _velocity = 0f;
    // Actions
    private bool _isGrounded;
    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (PlayerInputActions action in _playerInputs)
        {
            action.AssignedAction = InputSystem.actions.FindAction(action.Name + _playerNum);
            if (action.AssignedAction == null) { Debug.LogWarning("No action found with the name: " + action.Name + _playerNum); }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (PlayerInputActions act in _playerInputs)
        {
            if (act.AssignedAction.WasPressedThisFrame())
            {
                DoAction(act.Name);
            }
        }
        // Movement
        // Physics
        if (!grounded)
        {
            transform.position += new Vector3(0, velocity * Time.deltaTime, 0);
            velocity += Physics2D.gravity.y * Time.deltaTime;
        }
    }
    private void DoAction(string action)
    {
        switch(action)
        {
            case "Move": break;
            case "Block": break;
            case "Punch": Punch(); break;
            case "Jump": break;
            case "Crush": break;
            default: break;
        }
    }
    private void Move()
    {
        Vector2 dir = InputSystem.actions.FindAction("Move"+_playerNum).ReadValue<Vector2>();
        Vector3 origin = new(transform.position.x, transform.position.y, 0);
        if (!Physics2D.Raycast(origin, dir * Vector2.right, dir.magnitude))
        {
            transform.position += new Vector3(dir.x * _speed * Time.deltaTime, 0, 0);
        }
    }
    private void Punch()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        { _animator.SetTrigger("PunchTrigger"); }
    }
    private void Jump()
    {
        velocity += _jumpHeight;
        grounded = false;
    }
    // ---- Public methods ----
    #region Public methods
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
    #endregion
}
