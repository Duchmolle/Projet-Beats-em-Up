using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [Min(1)]
    [SerializeField] int _hitsToDamage;
    [SerializeField] Sprite[] _sprites;
    [SerializeField] SpriteRenderer _renderer;
    [SerializeField] DestructibleData _destructibleData;

    private int hitCount;
    private int spriteIndex = 0;

    private void Awake()
    {
        hitCount = 0;
        _renderer.sprite = _sprites[0];
        _destructibleData.isHit = this;
    }

    public void IsHit() //Methode a choper depuis player
    {
            hitCount++;
            if (hitCount >= _hitsToDamage && _sprites.Length > spriteIndex)
            {  
                _renderer.sprite = _sprites[spriteIndex++];
            }
    }


}
