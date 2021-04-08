using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HorizontalMovementState
{
    IDLE,
    WALK,
    JUMPUP,
    KO
}


public class StateMachine : MonoBehaviour
{
    #region Show In Inspector
    [SerializeField] PlayerAnimatorController _playerAnimatorController;
    [SerializeField] PlayerMovement _playerMovement;
    [SerializeField] PlayerAttack _playerAttack;
    [SerializeField] CombatStateMachine _combatStateMachine;
    #endregion

    #region Variables Globales
    HorizontalMovementState _currentState;
    #endregion

    #region On GUI
    /*private void OnGUI()
    {
        GUIStyle style = new GUIStyle("button");
        style.fontSize = 16;
        style.alignment = TextAnchor.MiddleLeft;

        using (new GUILayout.AreaScope(new Rect(Screen.width - 400, 50, 350, 100)))
        {
            using (new GUILayout.VerticalScope())
            {
                GUILayout.Button($"State: {_currentState}", style, GUILayout.ExpandHeight(true));
            }
        }
    }*/

    #endregion


    private void Update()
    {
        OnStateUpdate(_currentState);
    }

    #region State Machine

    private void OnStateEnter(HorizontalMovementState state)
    {
        switch (state)
        {
            case HorizontalMovementState.IDLE:
                DoIdleEnter();
                break;

            case HorizontalMovementState.WALK:
                DoWalkEnter();
                break;

            case HorizontalMovementState.JUMPUP:
                DoJumpUpEnter();
                break;
        }
    }

    private void OnStateExit(HorizontalMovementState state)
    {
        switch (state)
        {
            case HorizontalMovementState.IDLE:
                DoIdleExit();
                break;

            case HorizontalMovementState.WALK:
                DoWalkExit();
                break;

            case HorizontalMovementState.JUMPUP:
                DoJumpUpExit();
                break;
        }
    }

    private void OnStateUpdate(HorizontalMovementState state)
    {
        switch (state)
        {
            case HorizontalMovementState.IDLE:
                DoIdleUpdate();
                break;

            case HorizontalMovementState.WALK:
                DoWalkUpdate();
                break;

            case HorizontalMovementState.JUMPUP:
                DoJumpUpUpdate();
                break;
        }
    }

    private void TransitionToState(HorizontalMovementState fromState, HorizontalMovementState toState)
    {
        OnStateExit(fromState);
        _currentState = toState;
        OnStateEnter(toState);
    }

    #endregion

    #region Idle State
    private void DoIdleEnter()
    {
        _playerAnimatorController.EnterIdleAnimation();
    }

    private void DoIdleExit()
    {
        _playerAnimatorController.ExitIdleAnimation();
    }

    private void DoIdleUpdate()
    {
        if(_playerMovement.DirectionX > 0 || _playerMovement.DirectionY > 0)
        {
            TransitionToState(_currentState, HorizontalMovementState.WALK);
        }

        if (_playerMovement.DirectionX < 0 || _playerMovement.DirectionY < 0)
        {
            TransitionToState(_currentState, HorizontalMovementState.WALK);
        }

        if(_playerMovement.DirectionY > 0 && _playerMovement._isJump)
        {
            TransitionToState(_currentState, HorizontalMovementState.JUMPUP);
        }
    }
    #endregion

    #region Walk State
    private void DoWalkEnter()
    {
        _playerAnimatorController.EnterWalkAnimation();
    }

    private void DoWalkExit()
    {
        _playerAnimatorController.ExitWalkAnimation();
    }

    private void DoWalkUpdate()
    {
        if (Mathf.Approximately(_playerMovement.DirectionX, 0) && Mathf.Approximately(_playerMovement.DirectionY, 0))
        {
            TransitionToState(_currentState, HorizontalMovementState.IDLE);
        }
    }
    #endregion

    #region Jump State
    private void DoJumpUpEnter()
    {
        _playerAnimatorController.EnterJumpUpAnimation();
    }

    private void DoJumpUpExit()
    {
        _playerAnimatorController.ExitJumpUpAnimation();
    }

    private void DoJumpUpUpdate()
    {
        if(!_playerMovement._isJump)
        {
            TransitionToState(_currentState, HorizontalMovementState.IDLE);
        }
    }

    #endregion


}
