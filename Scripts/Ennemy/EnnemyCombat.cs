using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyCombat : MonoBehaviour
{

    [SerializeField] Transform _topLeft, _bottomRight;
    [SerializeField] LayerMask _whatIsPlayer;
    [SerializeField] int _ennemyHealth = 50;
    [SerializeField] EnnemyData dataHolder;
    [SerializeField] int dammage = 10;
    [SerializeField] GameObject _hitVFX;
    [SerializeField] private Transform _fists;
    [SerializeField] GameObject[] _lootPrefab;

    //ref ennemy data
    private Collider2D[] _buffer = new Collider2D[1];
    private bool isHurt;
    

    private void Awake()
    {
        dataHolder.body = this;
    }

    public int GetEnnemyLife()
    {     
        return _ennemyHealth;
    }

    public bool Hurt
    {
        get { return isHurt; }
        set { isHurt = value; }
    }

    public void HitByPlayer(int damage)
    {
        Debug.Log("I am hit by player !");
        _ennemyHealth -= damage;
        Hurt = true;
    }

    public void PunchPlayer()
    {

        if(Physics2D.OverlapAreaNonAlloc(_topLeft.position, _bottomRight.position, _buffer, _whatIsPlayer) > 0)
        {
            _buffer[0].gameObject.GetComponentInParent<PlayerAttack>().HitByEnnemy(dammage);
            Instantiate(_hitVFX, _fists.position, Quaternion.identity);
            //Debug.Log(gameObject.name + " hit the player !");
        }
        
    }

    public void DropLoot()
    {
        var spawnLoot = new Vector2(transform.position.x, transform.position.y) + Random.insideUnitCircle * 1.2f;
        Instantiate(_lootPrefab[Random.Range(0,_lootPrefab.Length)], spawnLoot, Quaternion.identity);
    }

    #region Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

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
