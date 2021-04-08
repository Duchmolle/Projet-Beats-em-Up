using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectHealth : MonoBehaviour
{
    [SerializeField] PlayerHP _player;
    [SerializeField] int _healAmount;

    public void DoCollect()
    {
        _player.health += _healAmount;
    }
}
