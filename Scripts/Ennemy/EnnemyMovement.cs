using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyMovement : MonoBehaviour
{
    [Header("Ennemy Parameters")]
    [Range(0,20)]
    [SerializeField] float _ennemySpeed;
    [SerializeField] float _pushForce;
    [SerializeField] float _destroyDelay;
    [Header("Components")]
    [SerializeField] Rigidbody2D _ennemyRb;
    [SerializeField] TransformData _playerPos;
    [Header("IA")]
    [SerializeField] Transform _topLeft;
    [SerializeField] Transform _bottomRight;
    [SerializeField] LayerMask _whatIsPlayer;
    [SerializeField] Color _gizmosColor;

    private bool _isMoving;
    private bool _isDead;
    private PlayerAttack _playerStatus;
    private Collider2D[] _buffer = new Collider2D[1];
    public bool IsMoving()
    {
        if (Mathf.Abs(_ennemyRb.velocity.x) > 0.01 || Mathf.Abs(_ennemyRb.velocity.y) > 0.01)
        {
            _isMoving = true;
        }
        else _isMoving = false;

        return _isMoving;
    }
    private Vector2 randomPos;

    public bool DetectPlayer()
    {
        int hitcount = Physics2D.OverlapAreaNonAlloc(_topLeft.position, _bottomRight.position, _buffer, _whatIsPlayer);
        return hitcount > 0;
    }

    private void Start()
    {
      randomPos = new Vector2(_playerPos.value.position.x+ Random.Range(0, 3), _playerPos.value.position.y+Random.Range(0, 3));
      _playerStatus = _playerPos.value.GetComponent<PlayerAttack>();
    }


    private void Update()
    {
        if (_isDead||_playerStatus.isDead) { return; }


        FlipSprite();  
    }

    private void FixedUpdate()
    {

    }

    public void StopAndPunch()
    {
        _ennemyRb.velocity = Vector2.zero;
    }

    public void TrackPlayer()
    {
        if (_playerPos.value != null)
        {
            Vector2 targetPos = _playerPos.value.position - transform.position;
            _ennemyRb.velocity = targetPos * _ennemySpeed;
        }
        else Debug.LogError(gameObject.name + "Player Data is missing !");
    }

    public void RunAway()
    {
         //Debug.Log("RUN !");
         Vector2 targetPos = _playerPos.value.position.normalized - transform.position;
        _ennemyRb.velocity = (randomPos * _ennemySpeed * Time.deltaTime); 

    }

    public void Knockback()
    {
        _ennemyRb.velocity = Vector2.zero;
        _ennemyRb.AddForce(-_playerPos.value.position* _pushForce,ForceMode2D.Impulse); // A paramétrer !
    }

    public void KO()
    {
        _isDead = true;
        _ennemyRb.velocity = Vector2.zero;
        Destroy(gameObject, _destroyDelay);
    }

    private void FlipSprite()
    {
        bool ennemyIsPast = Mathf.Sign(_playerPos.value.position.x - transform.position.x) > 0;
        if (ennemyIsPast)
        {
            GetComponentInChildren<Transform>().localScale = new Vector2(1, 1);
        }
        else GetComponentInChildren<Transform>().localScale = new Vector2(-1, 1);
    }



    #region Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmosColor;

        Vector2 topLeft = _topLeft.position;
        Vector2 topRight = new Vector2(_bottomRight.position.x, _topLeft.position.y);
        Vector2 bottomRight = _bottomRight.position;
        Vector2 bottomLeft = new Vector2(_topLeft.position.x, _bottomRight.position.y);

        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);
    }
    #endregion
}
