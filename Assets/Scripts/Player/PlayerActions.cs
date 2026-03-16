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
[RequireComponent(typeof(PlayerMovement))]
public class PlayerActions : MonoBehaviour
{
    // ------ Editor parametres ------
    #region Editor parametres
    [Header("Player info")]
    [SerializeField] private int _playerNum = 1;
    [Header("Player Components")]
    [SerializeField] private Animator _animator;
    [Header("Actions")]
    [SerializeField] private List<PlayerInputActions> _playerInputs = new List<PlayerInputActions>();
    #endregion
    private bool _isFalling = false; private bool _isBlocking = false; private bool isCrushing = false;
    private PlayerMovement pm;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pm = gameObject.GetComponent<PlayerMovement>();
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
            if (act.AssignedAction.IsPressed())
            {
                DoAction(act.Name);
            }
        }
    }
    /// <summary>
    /// Executes a given action
    /// </summary>
    /// <param name="action"></param>
    private void DoAction(string action)
    {
        switch (action)
        {
            case "Block": Block(); break;
            case "Punch": Punch(); break;
            case "Crush": Crush(); break;
            case "Kick": Kick(); break;
            default: break;
        }
    }

    // ---- Actions-attacks ----
    #region Attack Actions
    private void Punch()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        { _animator.SetTrigger("PunchTrigger"); }
    }
    private void Block()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        { _animator.SetTrigger("BlockTrigger"); }
    }
    private void Crush()
    {
        if (!pm.GetGroundedState() && (_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || _animator.GetCurrentAnimatorStateInfo(0).IsName("Walking")))
        {
            _animator.SetTrigger("FlipTrigger");
            pm.SetFallingState(true);
        }
    }
    private void Kick()
    {
        if (pm.GetGroundedState() && _animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            _animator.SetTrigger("KickTrigger");
        }
    }
    #endregion
    // ---- Public methods ----
    #region Public methods
    /// <summary>
    /// Returns the current player number
    /// </summary>
    /// <returns>Player number</returns>
    public int GetPlayerNumber()
    {
        return _playerNum;
    }
    /// <summary>
    /// Returns the current blocking state of the player
    /// </summary>
    /// <returns>Blocking state</returns>
    public bool GetBlockingState()
    {
        return _isBlocking;
    }
    public void SetBlockingState(bool set)
    {
        _isBlocking = set;
    }
    #endregion
}
