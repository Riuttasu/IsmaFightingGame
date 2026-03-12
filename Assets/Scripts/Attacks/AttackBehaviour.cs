using UnityEngine;

public class AttackBehaviour : StateMachineBehaviour
{
    [SerializeField] private string _attackName;
    [Header("Attack parametres")]
    [SerializeField] private float _attackStart;
    [SerializeField] private float _attackEnd;
    private bool _hitBoxState;
    private PlayerHitBoxManager hbm;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hbm = animator.gameObject.GetComponentInParent<PlayerHitBoxManager>();
        if (hbm == null)
        { Debug.LogError("Can't activate hitboxes if theres no manager"); Destroy(this); }
        else
        { _hitBoxState = (_attackStart != 0); }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // AnimTime toma un valor entre 0 y 1 dependiendo de en que momento de la animación se está
        float AnimTime = stateInfo.normalizedTime % 1f;
        bool OnTime = (AnimTime >= _attackStart && AnimTime <= _attackEnd);
        if (OnTime != _hitBoxState)
        {
            hbm.SetHitBox(_attackName, OnTime);
            _hitBoxState = OnTime;
        }

    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Deshabilita la hitbox una última vez por si la animación es interrumpida
        hbm.SetHitBox(_attackName, false);
    }
}
