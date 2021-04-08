using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    #region Show In Inspector
    [SerializeField] Rigidbody2D _playerRigidBody;
    [SerializeField] PlayerAttack _playerAttack;
    #endregion

    #region Variable Globales

    private Animator _playerAnimator;
    private int _directionXId = Animator.StringToHash("DirectionX");
    private int _directionYId = Animator.StringToHash("DirectionY");
    private int _isIdleId = Animator.StringToHash("isIdle");
    private int _isWalkId = Animator.StringToHash("isWalk");
    private int _isPunch1Id = Animator.StringToHash("isPunch1");
    private int _isPunch2Id = Animator.StringToHash("isPunch2");
    private int _isPunch3Id = Animator.StringToHash("isPunch3");
    private int _isPunch4Id = Animator.StringToHash("isPunch4");
    private int _isJumpId = Animator.StringToHash("isJump");
    private int _isHurtId = Animator.StringToHash("isHurt");
    private int _isKOId = Animator.StringToHash("isKO");

    #endregion

    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        _playerAnimator.SetFloat(_directionXId, _playerRigidBody.velocity.x);
        _playerAnimator.SetFloat(_directionYId, _playerRigidBody.velocity.y);
    }

    #region Public Methods
    //Ici on va récuperer toute les animations dans des variables publiques afin de les "donner" à notre State Machine

    #region Idle Animation
    public void EnterIdleAnimation()
    {
        _playerAnimator.SetBool(_isIdleId, true);
    }

    public void ExitIdleAnimation()
    {
        _playerAnimator.SetBool(_isIdleId, false);
    }
    #endregion

    #region Walk Animation
    public void EnterWalkAnimation()
    {
        _playerAnimator.SetBool(_isWalkId, true);
    }

    public void ExitWalkAnimation()
    {
        _playerAnimator.SetBool(_isWalkId, false);
    }
    #endregion

    #region Jump Animation

    public void EnterJumpUpAnimation()
    {

        _playerAnimator.SetBool(_isJumpId, true);
    }

    public void ExitJumpUpAnimation()
    {

        _playerAnimator.SetBool(_isJumpId, false);
    }

    #endregion

    #region Punch1 Animation
    public void EnterPunch1Animation()
    {
        _playerAnimator.SetBool(_isPunch1Id, true);
    }
    public void ExitPunch1Animation()
    {
        _playerAnimator.SetBool(_isPunch1Id, false);
    }
    #endregion

    #region Punch2 Animation

    public void EnterPunch2Animation()
    {
        _playerAnimator.SetBool(_isPunch2Id, true);
    }
    public void ExitPunch2Animation()
    {
        _playerAnimator.SetBool(_isPunch2Id, false);
    }

    #endregion

    #region Punch3 Animation

    public void EnterPunch3Animation()
    {
        _playerAnimator.SetBool(_isPunch3Id, true);
    }
    public void ExitPunch3Animation()
    {
        _playerAnimator.SetBool(_isPunch3Id, false);
    }

    #endregion

    #region Punch4 Animation

    public void EnterPunch4Animation()
    {
        _playerAnimator.SetBool(_isPunch4Id, true);
    }
    public void ExitPunch4Animation()
    {
        _playerAnimator.SetBool(_isPunch4Id, false);
    }

    #endregion

    #region Hurt Animation

    public void EnterHurtAnimation()
    {
        _playerAnimator.SetBool(_isHurtId, true);
    }

    public void ExitHurtAnimation()
    {
        _playerAnimator.SetBool(_isHurtId, false);
    }

    #endregion

    #region KO Animation
    
    public void EnterKOAnimation()
    {
        _playerAnimator.SetTrigger(_isKOId);
    }

    #endregion
    #endregion
}
