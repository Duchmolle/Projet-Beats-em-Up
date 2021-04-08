using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoucheIncendie : MonoBehaviour
{
    [SerializeField] Animator _anim;
    [SerializeField] int _hitsToDamage;

    private int hitCount;
    private int spriteIndex = 0;

    private void Awake()
    {
        hitCount = 0;
    }

    public void IsHit() //Methode a choper depuis player
    {
        hitCount++;
        if (hitCount >= _hitsToDamage)
        {
            _anim.SetBool("isBroken", true);
        }
    }

}
