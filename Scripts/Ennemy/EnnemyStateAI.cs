using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class EnnemyStateAI : MonoBehaviour
{


    [SerializeField] EnnemyMovement _ennemy;

    float _comeBackDelay , _runAwayDelay;
    private float _comeBackTime, _runAwayTime;
    private State _currentState;

    private void Awake()
    {
        _comeBackDelay = Random.Range(5, 7);
        _runAwayDelay = Random.Range(3, 6);
    }

    private void Start()
    {
        TransitionToState(_currentState, State.IDLE);
    }

    public enum State
    {
        TRACK, RUN, FIGHT, IDLE
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
            case State.TRACK:
                OnTrackEnter();
                break;
            case State.RUN:
                OnRunEnter();
                break;
            case State.FIGHT:
                OnFightEnter();
                break;
            case State.IDLE:
                OnIdleEnter();
                break;


        }
    }

    private void OnStateExit(State state)
    {
        switch (state)
        {
            case State.TRACK:
                OnTrackExit();
                break;
            case State.RUN:
                OnRunExit();
                break;
            case State.FIGHT:
                OnFightExit();
                break;
            case State.IDLE:
                OnIdleExit();
                break;

        }
    }

    private void OnStateUpdate(State state)
    {
        switch (state)
        {
            case State.TRACK:
                OnTrackUpdate();
                break;
            case State.RUN:
                OnRunUpdate();
                break;
            case State.FIGHT:
                OnFightUpdate();
                break;
            case State.IDLE:
                OnIdleUpdate();
                break;

        }
    }

    #endregion
    #region Track
    private void OnTrackEnter()
    {
        
    }

    void OnTrackUpdate()
    {
        _ennemy.TrackPlayer();
        if (_ennemy.DetectPlayer())
        {
            TransitionToState(_currentState, State.FIGHT);
        }
    }

    void OnTrackExit()
    {

    }

    #endregion
    #region Run

    private void OnRunEnter()
    {
        _comeBackTime = Time.time + _comeBackDelay;
    }

    void OnRunUpdate()
    {
        _ennemy.RunAway();

        if(_comeBackTime <= Time.time)
        {
            _ennemy.StopAndPunch();
            TransitionToState(_currentState, State.TRACK);
        }
    }

    void OnRunExit()
    {
        _comeBackTime = 0;
    }

    #endregion
    #region Fight
    private void OnFightEnter()
    {
        _ennemy.StopAndPunch();
        _runAwayTime = Time.time + _runAwayDelay;
    }

    void OnFightUpdate()
    {
        if (_runAwayTime <= Time.time)
        {
            TransitionToState(_currentState, State.RUN);
        }
        else if (!_ennemy.DetectPlayer())
        {
            TransitionToState(_currentState, State.TRACK);
        }
    }

    void OnFightExit()
    {
        _runAwayTime = 0;
    }
    #endregion
    #region Idle
    private void OnIdleEnter()
    {

    }

    void OnIdleUpdate()
    {
        if (!_ennemy.DetectPlayer())
        {
            TransitionToState(_currentState, State.TRACK);
        }
    }

    void OnIdleExit()
    {

    }
    #endregion

    private void TransitionToState(State fromState, State toState)
    {
        OnStateExit(fromState);
        _currentState = toState;
        OnStateEnter(toState);
    }

    private void OnGUI()
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
    }
}



