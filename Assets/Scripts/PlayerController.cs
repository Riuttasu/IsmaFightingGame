using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// Component which enables the movement and actions of a player, along with possible animations or sprite changes
/// Allows for two different players and can communicate with gamemanager for HP and gameover statuses
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Behaviour")]
    [SerializeField] private int playernum = 1;
    [Header("Attack Parametres")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpheight = 3f;
    // Input actions
    private InputAction punchAction, moveAction, blockAction, jumpAction, crushAction;
    // Method components
    private Animator anim; private SpriteRenderer sr;
    // Action / status bools
    private bool isDoingAction = false; private bool isblocking = false; private bool isCrushing = false;
    private bool grounded = true;
    // Jump physics
    private float velocity = 0f;
    private void Awake()
    {
        // Component references
        anim = GetComponent<Animator>(); // Animator
        sr = GetComponent<SpriteRenderer>(); // Sprite renderer
        // Player 1 input controls
        if (playernum == 1)
        {
            punchAction = InputSystem.actions.FindAction("Punch1");
            moveAction = InputSystem.actions.FindAction("Move1");
            blockAction = InputSystem.actions.FindAction("Block1");
            jumpAction = InputSystem.actions.FindAction("Jump1");
            crushAction = InputSystem.actions.FindAction("Crush1");
        }
        // Player 2 input controls
        else
        {
            punchAction = InputSystem.actions.FindAction("Punch2");
            moveAction = InputSystem.actions.FindAction("Move2");
            blockAction = InputSystem.actions.FindAction("Block2");
            jumpAction = InputSystem.actions.FindAction("Jump2");
        }
    }

    private void Update()
    {
        // Blocking input actions
        // Punch action
        if (punchAction != null && punchAction.WasPressedThisFrame() && !isDoingAction && !isblocking)
        {
            Punch();
        }
        // Block action
        else if (blockAction != null && blockAction.WasPressedThisFrame() && !isDoingAction && !isblocking)
        {
            Block();
        }
        // Move action
        else if (moveAction != null && moveAction.IsPressed() && !isDoingAction && !isblocking)
        {
            Move();
        }
        // Non-blocking input actions
        if (jumpAction.WasPressedThisFrame() && grounded)
        {
            Jump();
        }
        // Physics
        if (!grounded)
        {
            transform.position += new Vector3(0, velocity * Time.deltaTime, 0);
            velocity += Physics2D.gravity.y * Time.deltaTime;
            if(crushAction.WasPressedThisFrame())
            {
                Crush();
            }
        }
    }

    #region ACTIONS
    /// <summary>
    /// Block action, starts an animation trigger
    /// </summary>
    private void Block()
    {
        isblocking = true;
        if (anim != null) anim.SetTrigger("BlockTrigger");
    }
    private void EndBlock()
    {
        isblocking = false;
    }
    private void Move()
    {
        Vector2 dir = moveAction.ReadValue<Vector2>();
        Vector3 origin = new(transform.position.x, transform.position.y, 0);
        if (!Physics2D.Raycast(origin, dir * Vector2.right, dir.magnitude))
        {
            transform.position += new Vector3(dir.x * speed * Time.deltaTime, 0, 0);
        }
    }
    private void Punch()
    {
        isDoingAction = true;
        if (anim != null) anim.SetTrigger("PunchTrigger");
    }
    public void EndPunch()
    {
        isDoingAction = false;
    }
    private void Crush()
    {
        isDoingAction = true;
        if (anim != null) anim.SetTrigger("FlipTrigger");
    }

    #endregion
    #region jump
    private void Jump()
    {
        velocity += jumpheight;
        grounded = false;
    }
    public void Ground()
    {
        grounded = true;
        velocity = 0;
        if (isCrushing && anim != null)
        {
            anim.SetTrigger("CrushTrigger");
            isCrushing = false;
            isDoingAction = false;
        }
    }
    #endregion
    #region get
    public bool GetBlockingState()
    {
        return isblocking;
    }
    public int GetPlayerNumber()
    {
        return playernum;
    }
    #endregion
}
