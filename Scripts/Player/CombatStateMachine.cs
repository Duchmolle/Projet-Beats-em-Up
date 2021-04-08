using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CombatState
{
    IDLE,
    PUNCH1,
    PUNCH2,
    PUNCH3,
    PUNCH4,
    HURT
}
public class CombatStateMachine : MonoBehaviour
{
    #region Show In Inspector
    [SerializeField] PlayerAnimatorController _playerAnimatorController;
    [SerializeField] PlayerAttack _playerAttack;
    #endregion

    #region Variables Globales
    CombatState _currentState;
    #endregion
    private void Update()
    {
        OnStateUpdate(_currentState);
    }

    #region State Machine

    private void OnStateEnter(CombatState state)
    {
        switch (state)
        {

            case CombatState.PUNCH1:
                DoPunch1Enter();
                break;

            case CombatState.PUNCH2:
                DoPunch2Enter();
                break;

            case CombatState.PUNCH3:
                DoPunch3Enter();
                break;

            case CombatState.PUNCH4:
                DoPunch4Enter();
                break;

            case CombatState.HURT:
                DoHurtEnter();
                break;
        }
    }

    private void OnStateExit(CombatState state)
    {
        switch (state)
        {

            case CombatState.PUNCH1:
                DoPunch1Exit();
                break;

            case CombatState.PUNCH2:
                DoPunch2Exit();
                break;

            case CombatState.PUNCH3:
                DoPunch3Exit();
                break;

            case CombatState.PUNCH4:
                DoPunch4Exit();
                break;

            case CombatState.HURT:
                DoHurtExit();
                break;



        }
    }

    private void OnStateUpdate(CombatState state)
    {
        switch (state)
        {

            case CombatState.IDLE:
                DoIdleUpdate();
                break;

            case CombatState.PUNCH1:
                DoPunch1Update();
                break;

            case CombatState.PUNCH2:
                DoPunch2Update();
                break;

            case CombatState.PUNCH3:
                DoPunch3Update();
                break;

            case CombatState.PUNCH4:
                DoPunch4Update();
                break;

            case CombatState.HURT:
                DoHurtUpdate();
                break;
        }
    }

    private void TransitionToState(CombatState fromState, CombatState toState)
    {
        OnStateExit(fromState);
        _currentState = toState;
        OnStateEnter(toState);
    }

    #endregion

    #region Idle State


    private void DoIdleUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
        {

            TransitionToState(_currentState, CombatState.PUNCH1);
        }

        if (_playerAttack.isHurt)
        {
            TransitionToState(_currentState, CombatState.HURT);
            _playerAttack.isHurt = false;
        }

    }
    #endregion

    #region Punch1 State
    private void DoPunch1Enter()
    {
        _playerAnimatorController.EnterPunch1Animation();
        _playerAttack.Punch1();

    }

    private void DoPunch1Exit()
    {
        _playerAnimatorController.ExitPunch1Animation();
    }

    private void DoPunch1Update()
    {
        if (Input.GetButtonDown("Fire1") && _playerAttack.TimeEndCombo > Time.time)
        {
            TransitionToState(_currentState, CombatState.PUNCH2);            
        }

        if (_playerAttack.TimeEndCombo < Time.time)
        {
            TransitionToState(_currentState, CombatState.IDLE);            
        }

        if (_playerAttack.isHurt)
        {
            TransitionToState(_currentState, CombatState.HURT);
        }
    }

    #endregion

    #region Punch2 State
    private void DoPunch2Enter()
    {
        _playerAnimatorController.EnterPunch2Animation();
        _playerAttack.Punch2();
    }

    private void DoPunch2Exit()
    {
        _playerAnimatorController.ExitPunch2Animation();
    }

    private void DoPunch2Update()
    {
        if (Input.GetButtonDown("Fire1") && _playerAttack.TimeEndCombo > Time.time)
        {
            TransitionToState(_currentState, CombatState.PUNCH3);
        }

        if (_playerAttack.TimeEndCombo < Time.time)
        {
            TransitionToState(_currentState, CombatState.IDLE);
        }

        if (_playerAttack.isHurt)
        {
            TransitionToState(_currentState, CombatState.HURT);
        }
    }

    #endregion

    #region Punch3 State
    private void DoPunch3Enter()
    {
        _playerAnimatorController.EnterPunch3Animation();
        _playerAttack.Punch3();
    }

    private void DoPunch3Exit()
    {
        _playerAnimatorController.ExitPunch3Animation();
    }

    private void DoPunch3Update()
    {
        if (Input.GetButtonDown("Fire1") && _playerAttack.TimeEndCombo > Time.time)
        {
            TransitionToState(_currentState, CombatState.PUNCH4);
        }

        if (_playerAttack.TimeEndCombo < Time.time)
        {
            TransitionToState(_currentState, CombatState.IDLE);
        }

        if (_playerAttack.isHurt)
        {
            TransitionToState(_currentState, CombatState.HURT);
        }
    }

    #endregion

    #region Punch4 State
    private void DoPunch4Enter()
    {
        _playerAnimatorController.EnterPunch4Animation();
        _playerAttack.Punch4();
    }

    private void DoPunch4Exit()
    {
        _playerAnimatorController.ExitPunch4Animation();
    }

    private void DoPunch4Update()
    {
        if (Input.GetButtonDown("Fire1") && _playerAttack.TimeEndCombo > Time.time)
        {
            TransitionToState(_currentState, CombatState.PUNCH1);
        }

        if (_playerAttack.TimeEndCombo < Time.time)
        {
            TransitionToState(_currentState, CombatState.IDLE);
        }

        if (_playerAttack.isHurt)
        {
            TransitionToState(_currentState, CombatState.HURT);
        }
    }

    private void DoHurtEnter()
    {
        _playerAnimatorController.EnterHurtAnimation();
    }

    private void DoHurtExit()
    {
            _playerAttack.isHurt = false;
            _playerAnimatorController.ExitHurtAnimation();
    }

    private void DoHurtUpdate()
    {
            TransitionToState(_currentState, CombatState.IDLE);
        
    }
    

    #endregion
}
