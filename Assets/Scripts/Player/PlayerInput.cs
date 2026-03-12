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
public class PlayerInput : MonoBehaviour
{
    // Player number 
    [SerializeField] private int playernum = 1;
    // Input actions
    [SerializeField] private List<PlayerInputActions> _playerInputs = new List<PlayerInputActions>();
    // Player input component
    private PlayerActions pa;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pa = gameObject.GetComponent<PlayerActions>();
        if (pa == null) { Debug.LogError("No component to relay actions to"); Destroy(this); }
        else
        {
            foreach (PlayerInputActions action in _playerInputs)
            {
                action.AssignedAction = InputSystem.actions.FindAction(action.Name + playernum);
                if (action.AssignedAction == null) { Debug.LogWarning("No action found with the name: " + action.Name + playernum); }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        string Action = " ";
        foreach (PlayerInputActions action in _playerInputs)
        {
            if (action.AssignedAction.WasPressedThisFrame())
            {
                Action = action.Name;
            }
        }
        
    }
}
