using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    #region Show In Inspector
    [SerializeField] PlayerAnimatorController _playerAnimatorController;
    [SerializeField] HurtBox _hurtBox;
    [SerializeField] float _comboDelay;
    [SerializeField] EnnemyData _ennemyCombat;
    [SerializeField] PlayerHP _playerCurrentHP;
    [SerializeField] PlayerHP _playerMaxHP;
    [SerializeField] DestructibleData _destructibleData;
    [SerializeField] GameObject _hitVFX;
    [SerializeField] Transform _fists;
    [SerializeField] PlayerHP _playerCurrentLives;
    [SerializeField] PlayerHP _playerMaxLives;


    [Header("Damage made")]
    [SerializeField] int _firstPunchDamage;
    [SerializeField] int _secondPunchDamage;
    [SerializeField] int _thirdPunchDamage;
    [SerializeField] int _fourthPunchDamage;
    #endregion

    #region Variables Globales
    private LevelManager _levelManager;
    float _timeEndCombo;
    bool _isHurt;
    Destructible _destructibleTarget;
    EnnemyCombat _ennemyTarget;
    Rigidbody2D _playerRigidbody;
    bool _isDead;

    #endregion

    

    #region Public Properties

    public float TimeEndCombo
    {
        get
        {
            return _timeEndCombo;
        }
    }

    public bool isHurt
    {
        get
        {
           return _isHurt;
        }
        set
        {
            _isHurt = value;
        }
    }

    public bool isDead
    {
        get { return _isDead; }

        set => _isDead = value;
    }

    #endregion

    private void Awake()
    {
        _playerCurrentHP.health = _playerMaxHP.health;
        _playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        LevelManager[] tab = Resources.FindObjectsOfTypeAll<LevelManager>();
        _levelManager = tab[0];
    }

    #region Public Methods

    #region Combo Methods
    public void Punch1()
    {            
        _timeEndCombo = Time.time + _comboDelay;

        if (_hurtBox.ICanPunchThat())
        {
            if (_hurtBox._overlapHitBuffer[0].CompareTag("Ennemy"))
            {
                Debug.Log("Aïe Aïe Ouuuuille");
                Instantiate(_hitVFX, _fists.position, Quaternion.identity);
                _ennemyTarget = _hurtBox._overlapHitBuffer[0].GetComponent<EnnemyCombat>();
                _ennemyTarget.HitByPlayer(_firstPunchDamage);
            }

            else if (_hurtBox._overlapHitBuffer[0].CompareTag("Destructible"))
            {
                Debug.Log("Hit un destructible");
                Instantiate(_hitVFX, _fists.position, Quaternion.identity);
                _destructibleTarget = _hurtBox._overlapHitBuffer[0].GetComponent<Destructible>();
                _destructibleTarget.IsHit();
            }
        }
    }

    public void Punch2()
    {

        _timeEndCombo = Time.time + _comboDelay;

        if (_hurtBox.ICanPunchThat())
        {
            if (_hurtBox._overlapHitBuffer[0].CompareTag("Ennemy"))
            {
                Debug.Log("PunchEnnemy X 2");
                Instantiate(_hitVFX, _fists.position, Quaternion.identity);
                _ennemyTarget = _hurtBox._overlapHitBuffer[0].GetComponent<EnnemyCombat>();
                _ennemyTarget.HitByPlayer(_secondPunchDamage);
            }
            else if (_hurtBox._overlapHitBuffer[0].CompareTag("Destructible"))
            {
                Debug.Log("Hit un destructible X 2");
                Instantiate(_hitVFX, _fists.position, Quaternion.identity);
                _destructibleTarget.IsHit();
            }
        }
    }

    public void Punch3()
    {
        _timeEndCombo = Time.time + _comboDelay;

        if (_hurtBox.ICanPunchThat())
        {
            if (_hurtBox._overlapHitBuffer[0].CompareTag("Ennemy"))
            {
                Debug.Log("PunchEnnemy X 3");
                Instantiate(_hitVFX, _fists.position, Quaternion.identity);
                _ennemyTarget = _hurtBox._overlapHitBuffer[0].GetComponent<EnnemyCombat>();
                _ennemyTarget.HitByPlayer(_thirdPunchDamage);
            }
            else if (_hurtBox._overlapHitBuffer[0].CompareTag("Destructible"))
            {
                Debug.Log("Hit un destructible X 3");
                Instantiate(_hitVFX, _fists.position, Quaternion.identity);
                _destructibleTarget.IsHit();
            }
        }
    }

    public void Punch4()
    {
        _timeEndCombo = Time.time + _comboDelay;

        if (_hurtBox.ICanPunchThat())
        {
            if (_hurtBox._overlapHitBuffer[0].CompareTag("Ennemy"))
            {
                Debug.Log("PunchEnnemy X 4");
                Instantiate(_hitVFX, _fists.position, Quaternion.identity);
                _ennemyTarget = _hurtBox._overlapHitBuffer[0].GetComponent<EnnemyCombat>();
                _ennemyTarget.HitByPlayer(_fourthPunchDamage);
            }
            else if (_hurtBox._overlapHitBuffer[0].CompareTag("Destructible"))
            {
                Debug.Log("Hit un destructible X 4");
                Instantiate(_hitVFX, _fists.position, Quaternion.identity);
                _destructibleTarget.IsHit();
            }
        }

    }
    #endregion


    public void HitByEnnemy(int damage)
    {
        _playerCurrentHP.health -= damage;
        _isHurt = true;

        if (_playerCurrentHP.health <= 0)
        {
            _playerAnimatorController.EnterKOAnimation();
            _isDead = true;
            StartCoroutine(WaitAndDie());
            if (_playerCurrentLives.lives > 0)
            {
                _playerCurrentHP.health = _playerMaxHP.health;
            }
        }
    }

    private IEnumerator WaitAndDie()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("Waiting before respawn");
        _levelManager.ResetPlayer();
        gameObject.SetActive(false);
    }



    #endregion
}



