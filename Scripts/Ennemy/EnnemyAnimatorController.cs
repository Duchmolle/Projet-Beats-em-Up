using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyAnimatorController : MonoBehaviour
{
    [SerializeField] EnnemyMovement _ennemy;
    [SerializeField] Animator _ennemyAnim;

    void Start()
    {
        
    }

    void Update()
    {
        _ennemyAnim.SetBool(isMovingId, _ennemy.IsMoving());
    }

    #region Public walk
    public void EnterStateWalk()
    {

    }
    public void ExitStateWalk()
    {

    }
    #endregion
    #region Public Idle
    public void EnterStateIdle()
    {

    }
    public void ExitStateIdle()
    {

    }
    #endregion
    #region Public Punch
    public void EnterStatePunch()
    {
        _ennemyAnim.SetBool(isAttackingId, true);
    }
    public void ExitStatePunch()
    {
        _ennemyAnim.SetBool(isAttackingId, false);
    }
    #endregion
    #region Public KO
    public void EnterKOState()
    {
        _ennemyAnim.SetTrigger(isDeadId);
    }
    public void ExitStateKO()
    {

    }
    #endregion
    #region Public Hurt
    public void EnterHurtState()
    {
        _ennemyAnim.SetBool(hitId, true);
    }
    public void ExitHurtState()
    {
        _ennemyAnim.SetBool(hitId, false);
    }
    #endregion
    #region Public Knockback
    public void EnterKnokbackState()
    {
        _ennemyAnim.SetTrigger(hitDiveId);
    }
    public void ExitKnockbackState()
    {

    }
    #endregion

    private int isMovingId = Animator.StringToHash("isMoving");
    private int hitId = Animator.StringToHash("Hurt");
    private int hitDiveId = Animator.StringToHash("HitDive");
    private int isAttackingId = Animator.StringToHash("isAttacking");
    private int isDeadId = Animator.StringToHash("isDead");
}
