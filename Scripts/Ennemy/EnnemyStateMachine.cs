using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyStateMachine : MonoBehaviour
{
    [SerializeField] EnnemyAnimatorController _anim;
    [SerializeField] EnnemyMovement _ennemy;
    [SerializeField] EnnemyCombat _fight;

    private State _currentState;

    public enum State
    {
        WALK, IDLE, PUNCH, KO, HURT, KNOCKBACK
    }

    public State CurrentState
    {
        get
        {
            return _currentState;
        }
    }

    void Update()
    {
        OnStateUpdate(_currentState);
    }

    #region State Machine
    private void OnStateEnter(State state)
    {
        switch (state)
        {
            case State.WALK:
                OnWalkEnter();
                break;
            case State.IDLE:
                OnIdleEnter();
                break;
            case State.PUNCH:
                OnPunchEnter();
                break;
            case State.KO:
                OnKOEnter();
                break;
            case State.HURT:
                OnHurtEnter();
                break;
            case State.KNOCKBACK:
                OnKnockbackEnter();
                break;
        }
    }

    private void OnStateExit(State state)
    {
        switch (state)
        {
            case State.WALK:
                OnWalkExit();
                break;
            case State.IDLE:
                OnIdleExit();
                break;
            case State.PUNCH:
                OnPunchExit();
                break;
            case State.KO:
                OnKOExit();
                break;
            case State.HURT:
                OnHurtExit();
                break;
            case State.KNOCKBACK:
                OnKnockbackExit();
                break;
        }
    }

    private void OnStateUpdate(State state)
    {
        switch (state)
        {
            case State.WALK:
                OnWalkUpdate();
                break;
            case State.IDLE:
                OnIdleUpdate();
                break;
            case State.PUNCH:
                OnPunchUpdate();
                break;
            case State.KO:
                OnKOUpdate();
                break;
            case State.HURT:
                OnHurtUpdate();
                break;
            case State.KNOCKBACK:
                OnKnockbackUpdate();
                break;
        }
    }

    #endregion
    #region Walk
    private void OnWalkEnter()
    {
        _anim.EnterStateWalk();
    }

    void OnWalkUpdate()
    {
        if (_ennemy.DetectPlayer())
        {
            TransitionToState(_currentState, State.PUNCH);
        }
        if (!_ennemy.IsMoving())
        {
            TransitionToState(_currentState, State.IDLE);
        }
        if (_fight.GetEnnemyLife() <= 0)
        {
            TransitionToState(_currentState, State.KO);
        }
        if (_fight.Hurt)
        {
            _fight.Hurt = false;
            TransitionToState(_currentState, State.HURT);
        }
    }

    void OnWalkExit()
    {
        _anim.ExitStateWalk();
    }

    #endregion
    #region Idle

    private void OnIdleEnter()
    {
        _anim.EnterStateIdle();
    }

    void OnIdleUpdate()
    {
        if (_ennemy.DetectPlayer())
        {
            TransitionToState(_currentState, State.PUNCH);
        }
        if (_fight.GetEnnemyLife() <= 0)
        {
            TransitionToState(_currentState, State.KO);
        }
        if (_fight.Hurt)
        {
            _fight.Hurt = false;
            TransitionToState(_currentState, State.HURT);
        }
    }

    void OnIdleExit()
    {
        _anim.ExitStateIdle();
    }

    #endregion
    #region Punch

    private void OnPunchEnter()
    {
        _anim.EnterStatePunch();
    }

    private void OnPunchUpdate()
    {
        if (!_ennemy.DetectPlayer())
        {
            _ennemy.TrackPlayer();
        }

        else if (_ennemy.IsMoving())
        {
            TransitionToState(_currentState, State.WALK);
        }

        else if (_fight.GetEnnemyLife() <= 0)
        {
            TransitionToState(_currentState, State.KO);
        }
        else if (_fight.Hurt)
        {
            _fight.Hurt = false;
            TransitionToState(_currentState, State.HURT);
        }
    }

    private void OnPunchExit()
    {
        _anim.ExitStatePunch();
    }

    #endregion
    #region KO

    private void OnKOEnter()
    {
        _anim.EnterKOState();
        _fight.DropLoot();
    }

    private void OnKOUpdate()
    {
        _ennemy.KO();
    }

    private void OnKOExit()
    {
        _anim.ExitStateKO();
    }

    #endregion
    #region Hurt
    private void OnHurtEnter()
    {
        _anim.EnterHurtState();
    }

    private void OnHurtUpdate()
    {
        if (_fight.GetEnnemyLife() <= 0)
        {
            TransitionToState(_currentState, State.KO);
        }
        else TransitionToState(_currentState, State.IDLE);
    }

    private void OnHurtExit()
    {
        _anim.ExitHurtState();
    }

    #endregion
    #region Knockback

    private void OnKnockbackEnter()
    {
        _anim.EnterKnokbackState();
    }

    private void OnKnockbackUpdate()
    {
        _ennemy.Knockback();
    }

    private void OnKnockbackExit()
    {
        _anim.ExitKnockbackState();
    }
    #endregion

    private void TransitionToState(State fromState, State toState)
    {
        OnStateExit(fromState);
        _currentState = toState;
        OnStateEnter(toState);
    }

    /*private void OnGUI()
    {
        GUIStyle style = new GUIStyle("button");
        style.fontSize = 18;
        style.alignment = TextAnchor.MiddleLeft;

        using (new GUILayout.AreaScope(new Rect(Screen.width - 600, 300, 200, 100)))
        {
            using (new GUILayout.VerticalScope())
            {
                GUILayout.Button($"State: {_currentState}", style, GUILayout.ExpandHeight(true));
            }
        }
    }*/
}


